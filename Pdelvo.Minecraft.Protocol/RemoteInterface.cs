using System;
using System.Threading;
using Pdelvo.Minecraft.Protocol.Packets;
using System.Diagnostics;
using java.security;
using Pdelvo.Minecraft.Network;
using System.Collections;
using System.Collections.Generic;
using Pdelvo.Minecraft.Protocol.Helper;
using System.Linq;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public abstract class RemoteInterface : IMinecraftRemoteInterface, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected Thread Thread { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool _aborting;

        LockFreeQueue<Dieable<Packet>> _fastQueue = new LockFreeQueue<Dieable<Packet>>();
        LockFreeQueue<Dieable<Packet>> _slowQueue = new LockFreeQueue<Dieable<Packet>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteInterface"/> class.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        /// <remarks></remarks>
        protected RemoteInterface(PacketEndPoint endPoint)
        {
            Thread = new Thread(WriteLoop);
            EndPoint = endPoint;
        }

        #region IMinecraftRemoteInterface Members

        /// <summary>
        /// Sends the packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification="The general exception is passed to a event")]
        public void SendPacket(Packet packet)
        {
            try{
                EndPoint.SendPacket(packet);
            }
            catch (ThreadAbortException)
            {
                if (Aborted != null)
                    Aborted(this, new RemoteInterfaceAbortedEventArgs());
            }
            catch (Exception ex)
            {
                if (Aborted != null)
                    Aborted(this, new RemoteInterfaceAbortedEventArgs(ex));
            }
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Run()
        {
            if (Thread.ThreadState == System.Threading.ThreadState.Running)
                throw new InvalidOperationException("Thread already running");
            BeginReceivePacket();
            Thread.Start();
        }

        /// <summary>
        /// Registers the packet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <remarks></remarks>
        public void RegisterPacket<T>(byte id) where T : Packet, new()
        {
            EndPoint.RegisterPacket<T>(id);
        }

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <remarks></remarks>
        public PacketEndPoint EndPoint { get; private set; }

        /// <summary>
        /// Occurs when [packet received].
        /// </summary>
        /// <remarks></remarks>
        public event EventHandler<PacketEventArgs> PacketReceived;
        /// <summary>
        /// Occurs when [aborted].
        /// </summary>
        /// <remarks></remarks>
        public event EventHandler<RemoteInterfaceAbortedEventArgs> Aborted;

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Shutdown()
        {
            if (!_aborting)
            {
                _aborting = true;
                _writeEvent.Set();
                if (Aborted != null)
                    Aborted(this, new RemoteInterfaceAbortedEventArgs());
            }
            EndPoint.Shutdown();
        }

        #endregion

        ///// <summary>
        ///// Runs the loop.
        ///// </summary>
        ///// <remarks></remarks>
        //private void RunLoop()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            if (_aborting) return;
        //            ReadPacket();
        //        }
        //    }
        //    catch (ThreadAbortException)
        //    {
        //        if (Aborted != null)
        //            Aborted(this, new RemoteInterfaceAbortedEventArgs());
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Aborted != null)
        //            Aborted(this, new RemoteInterfaceAbortedEventArgs(ex));
        //    }
        //}

        AutoResetEvent _writeEvent = new AutoResetEvent(false);

        private void WriteLoop()
        {
            while (true)
            {
                _writeEvent.WaitOne();
                if (_aborting) return;
                Dieable<Packet> packet;
               
                if (_fastQueue.TryDequeue(out packet))
                {
                    //write data
                    _writeEvent.Set();
                    if (!packet.IsDead)
                    SendPacket(packet);
                }
                if (packet == null)
                {
                    if (_slowQueue.TryDequeue(out packet))
                    {
                        //write data
                        _writeEvent.Set();
                        if (!packet.IsDead)
                            SendPacket(packet);
                        //else Debug.WriteLine("Packet dropped");
                    }
                }
            }
        }

        public void SendPacketQueued(Packet packet, bool fast)
        {
            if (fast)
            {
                _fastQueue.Enqueue(packet);
                _writeEvent.Set();
            }
            else
            {
                var pc = packet as PreChunk;
                var mc = packet as MapChunk;
                if (mc != null)
                {
                    _slowQueue.EnumerateItems().Where(t => t.Item is MapChunk).Each(a => a.Die(r => 
                    { 
                        var mapChunk = r as MapChunk;
                        return mapChunk.PositionX == mc.PositionX && mapChunk.PositionZ == mc.PositionZ; 
                    }));
                    //_slowQueue.EnumerateItems().Where(t => t.Item is PreChunk).Each(a => a.Die(r => (r as PreChunk).X == mc.X && (r as PreChunk).Z == mc.Z));
                } 
                else if (pc != null)
                {
                    _slowQueue.EnumerateItems().Where(t => t.Item is MapChunk).Each(a => a.Die(r =>
                    {
                        var mapChunk = r as MapChunk;
                        return mapChunk.PositionX == pc.PositionX && mapChunk.PositionZ == pc.PositionZ;
                    }));
                }

                _slowQueue.Enqueue(packet);
                _writeEvent.Set();
            }
        }

        public void SendPacketQueued(Packet packet)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");
            SendPacketQueued(packet, !packet.CanBeDelayed);
        }

        /// <summary>
        /// Begins the receive packet.
        /// </summary>
        /// <remarks></remarks>
        private void BeginReceivePacket()
        {
            var buff = new byte[1];
            EndPoint.Stream.BeginRead(buff, 0, 1, ReadCompleted, buff);
        }

        /// <summary>
        /// Reads the completed.
        /// </summary>
        /// <param name="a">A.</param>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification="Exception is redirected to a event")]
        private void ReadCompleted(IAsyncResult a)
        {
            if (a.IsCompleted)
            {
            try
            {
                EndPoint.Stream.EndRead(a);
                Packet p = EndPoint.ReadPacket(((byte[]) a.AsyncState)[0]);
                if (EndPoint.Stream.BufferEnabled)
                    p.Data = EndPoint.Stream.GetBuffer();
                if (PacketReceived != null)
                    PacketReceived(this, new PacketEventArgs(p));
                BeginReceivePacket();
            }
            catch (ThreadAbortException)
            {
                if (Aborted != null)
                    Aborted(this, new RemoteInterfaceAbortedEventArgs());
            }
            catch (Exception ex)
            {
                if (Aborted != null)
                    Aborted(this, new RemoteInterfaceAbortedEventArgs(ex));
            }
            }
        }

        /// <summary>
        /// Reads the packet.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public Packet ReadPacket()
        {
            Packet p = EndPoint.ReadPacket();
            if (EndPoint.Stream.BufferEnabled)
                p.Data = EndPoint.Stream.GetBuffer();
            if (PacketReceived != null)
                PacketReceived(this, new PacketEventArgs(p));
            return p;
        }

        public void SwitchToRC4Mode(byte[] key)
        {
            var stream = EndPoint.Stream.Net as FullyReadStream;
            if (stream.BaseStream is RC4Stream)
            {
                //var rc4 = stream.BaseStream as RC4Stream;
                //rc4.Key = key;
            }
            else
            {
                stream.BaseStream = new RC4Stream(stream.BaseStream, key);
            }
        }
        public void SwitchToHCMode(byte [] key)
        {
            var stream = EndPoint.Stream.Net as FullyReadStream;
            if (stream.BaseStream is HCStream)
            {
                //var hc4 = stream.BaseStream as HCStream;
                //hc4.Key = key;
            }
            else
            {
                stream.BaseStream = new HCStream(stream.BaseStream, key);
            }
        }
        public void SwitchToAesMode(byte[] key)
        {
            var stream = EndPoint.Stream.Net as FullyReadStream;
            if (stream.BaseStream is AesStream)
            {
                //var hc4 = stream.BaseStream as AesStream;
                //hc4.Key = key;
            }
            else
            {
                stream.BaseStream = new AesStream(stream.BaseStream, key);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RemoteInterface()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._writeEvent.Dispose();
                this._writeEvent = null;
            }
        }

    }
}