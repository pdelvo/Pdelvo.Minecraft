using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public abstract class Packet
    {
        /// <summary>
        ///   Gets or sets the code.
        /// </summary>
        /// <value> The code. </value>
        /// <remarks>
        /// </remarks>
        public byte Code { get; protected set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="Packet" /> is cache.
        /// </summary>
        /// <value> <c>true</c> if cache; otherwise, <c>false</c> . </value>
        /// <remarks>
        /// </remarks>
        public bool Cache { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="Packet" /> is changed.
        /// </summary>
        /// <value> <c>true</c> if changed; otherwise, <c>false</c> . </value>
        /// <remarks>
        /// </remarks>
        public bool Changed { get; set; }

        /// <summary>
        ///   Gets or sets the data.
        /// </summary>
        /// <value> The data. </value>
        /// <remarks>
        /// </remarks>
        public IEnumerable<byte> Data { get; set; }

        public virtual bool CanBeDelayed
        {
            get { return false; }
        }

        /// <summary>
        ///   Sends the specified stream.
        /// </summary>
        /// <param name="stream"> The stream. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        public void Send(BigEndianStream stream, int version)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            OnSend(stream, version);
        }

        /// <summary>
        ///   Receives the specified reader.
        /// </summary>
        /// <param name="reader"> The reader. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        public void Receive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            OnReceive(reader, version);
        }

        public Task ReceiveAsync(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            return OnReceiveAsync(reader, version);
        }

        protected abstract void OnSend(BigEndianStream writer, int version);
        protected abstract void OnReceive(BigEndianStream reader, int version);

        protected virtual Task OnSendAsync(BigEndianStream writer, int version)
        {
            OnSend(writer, version);
            return Task.FromResult(0);
        }

        protected virtual Task OnReceiveAsync(BigEndianStream reader, int version)
        {
            OnReceive(reader, version);
            return Task.FromResult(0);
        }

        /// <summary>
        ///   Sends the item.
        /// </summary>
        /// <param name="stream"> The stream. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        public void SendItem(BigEndianStream stream, int version)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (Data != null && !Changed && stream.BufferEnabled)
            {
                stream.Write(Data.ToArray (), 0, Data.Count ());
            }
            else
                OnSend(stream, version);
            stream.Flush ();
        }

        public async Task SendItemAsync(BigEndianStream stream, int version)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (Data != null && !Changed && stream.BufferEnabled)
            {
                await stream.WriteAsync(Data.ToArray (), 0, Data.Count ());
            }
            else
                await OnSendAsync(stream, version);
            await stream.FlushAsync ();
        }
    }
}