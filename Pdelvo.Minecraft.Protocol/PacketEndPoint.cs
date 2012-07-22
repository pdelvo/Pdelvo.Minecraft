using System;
using System.Collections.Generic;
using System.Linq;
using Pdelvo.Minecraft.Protocol.Helper;
using Pdelvo.Minecraft.Protocol.Packets;
using Pdelvo.Minecraft.Network;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// Base class to handle a connection to a minecraft server or client
    /// </summary>
    /// <remarks></remarks>
    public class PacketEndPoint
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Dictionary<Type, Attribute[]> CustomAttributes = new Dictionary<Type, Attribute[]>();

        /// <summary>
        /// 
        /// </summary>
        private static readonly Dictionary<Type, LockFreeQueue<Packet>> PacketCache =
            new Dictionary<Type, LockFreeQueue<Packet>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly BigEndianStream _innerStream;
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<byte, List<Type>> _packets;
        /// <summary>
        /// 
        /// </summary>
        private readonly object _readLock = new object();
        /// <summary>
        /// 
        /// </summary>
        private readonly object _writeLock = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="PacketEndPoint"/> class from being created.
        /// </summary>
        /// <remarks></remarks>
        private PacketEndPoint()
        {
            _packets = new Dictionary<byte, List<Type>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEndPoint"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        public PacketEndPoint(BigEndianStream stream, int version)
            : this()
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            Version = version;
            stream.Owner = this;
            _innerStream = stream;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEndPoint"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        public PacketEndPoint(PacketEndPoint parent, BigEndianStream stream, int version)
            : this()
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            Version = version;
            _innerStream = stream;
            foreach (var item in parent._packets)
            {
                _packets.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        /// <remarks></remarks>
        public int Version { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [use cache].
        /// </summary>
        /// <value><c>true</c> if [use cache]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool UseCache { get; set; }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <remarks></remarks>
        public BigEndianStream Stream
        {
            get { return _innerStream; }
        }

        /// <summary>
        /// Gets the packets.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification="This method is doing a long running operation -> Linq query")]
        public IEnumerable<Type> GetPackets()
        {
            return _packets.SelectMany(item => item.Value);
        }

        /// <summary>
        /// Gets the cached item count.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This method is doing a long running operation -> Linq query")]
        public static int GetCachedItemCount()
        {
            return (from x in GetCachedItems()
                    select x.Item2).Sum();
        }

        /// <summary>
        /// Gets the cached items.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification="Made to generade json output."),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This method is doing a long running operation -> Linq query")]
       public static IEnumerable<Tuple<Type, int>> GetCachedItems()
        {
            return from x in PacketCache
                   where x.Value.Count != 0
                   select new Tuple<Type, int>(x.Key, x.Value.Count);
        }

        /// <summary>
        /// Registers the packet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification="For compile time validation")]
        public void RegisterPacket<T>(byte id) where T : Packet, new()
        {
            //if (_packets.ContainsKey(id))
            //    throw new ArgumentException("Id already exists: " + id.ToString("X"), "id");
            if (_packets.ContainsKey(id))
                _packets[id].Add(typeof (T));
            else
                _packets.Add(id, new List<Type> {typeof (T)});
        }

        /// <summary>
        /// Reads the packet.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public Packet ReadPacket(byte id)
        {
            lock (_readLock)
            {
                if (!_packets.ContainsKey(id))
                    throw new PacketException(id);

                IEnumerable<Type> packets = from b in _packets[id]
                                            where PacketSupportVersion(b) == true
                                            //because it is nullable bool
                                            select b;
                Type packet = packets.First();
                Packet p;
                if (UseCache)
                {
                    if (!PacketCache.ContainsKey(packet))
                    {
                        lock (PacketCache)
                        {
                            if (!PacketCache.ContainsKey(packet))
                            {
                                PacketCache.Add(packet, new LockFreeQueue<Packet>());
                            }
                        }
                    }
                    LockFreeQueue<Packet> queue = PacketCache[packet];
                    if (!queue.TryDequeue(out p))
                    {
                        PacketFactory.PacketCreator creator = PacketFactory.GetCreator(packet);
                        p = creator();
                    }
                }
                else
                {
                    PacketFactory.PacketCreator creator = PacketFactory.GetCreator(packet);
                    p = creator();
                }
                p.Receive(_innerStream, Version);
                p.Cache = true;
                p.Data = _innerStream.GetBuffer();
                return p;
            }
        }

        public async Task<Packet> ReadPacketAsync(byte id, bool useLock = true)
        {
            //if (useLock)
            //    Monitor.Enter(_readLock);
            //try
            //{
                if (!_packets.ContainsKey(id))
                    throw new PacketException(id);

                IEnumerable<Type> packets = from b in _packets[id]
                                            where PacketSupportVersion(b) == true
                                            //because it is nullable bool
                                            select b;
                Type packet = packets.First();
                Packet p;
                if (UseCache)
                {
                    if (!PacketCache.ContainsKey(packet))
                    {
                        lock (PacketCache)
                        {
                            if (!PacketCache.ContainsKey(packet))
                            {
                                PacketCache.Add(packet, new LockFreeQueue<Packet>());
                            }
                        }
                    }
                    LockFreeQueue<Packet> queue = PacketCache[packet];
                    if (!queue.TryDequeue(out p))
                    {
                        PacketFactory.PacketCreator creator = PacketFactory.GetCreator(packet);
                        p = creator();
                    }
                }
                else
                {
                    PacketFactory.PacketCreator creator = PacketFactory.GetCreator(packet);
                    p = creator();
                }
                await p.ReceiveAsync(_innerStream, Version);
                p.Cache = true;
                p.Data = _innerStream.GetBuffer();
                return p;
            //}
            //finally
            //{
            //    if (useLock)
            //        Monitor.Exit(_readLock);
            //}
        }

        /// <summary>
        /// Reads the packet.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public Packet ReadPacket()
        {
            lock (_readLock)
            {
                byte id = _innerStream.ReadByte();
                return ReadPacket(id);
            }
        }

        [DebuggerStepThrough]
        public async Task<Packet> ReadPacketAsync()
        {
            Monitor.Enter(_readLock);
            try
            {
                byte id = await _innerStream.ReadByteAsync();
                return await ReadPacketAsync(id, false);
            }
            finally
            {
                Monitor.Exit(_readLock);
            }
        }

        /// <summary>
        /// Packets the support version.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool? PacketSupportVersion(Type packet)
        {
            return IsPacketSupported(packet, Version);
        }

        /// <summary>
        /// Determines whether [is packet supported] [the specified packet].
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        public static bool? IsPacketSupported(Type packet, int version)
        {
            bool throwOnRequired;
            bool throwOnLast;
            int requiredVersion = GetRequiredVersion(packet, out throwOnRequired);
            int lastSupportedVersion = GetLastSupportedVersion(packet, out throwOnLast);

            bool require = (requiredVersion == -1 || requiredVersion <= version);
            bool last = (lastSupportedVersion == -1 || lastSupportedVersion >= version);
            if (!require && throwOnRequired) return null;
            if (!last && throwOnLast) return null;
            return require && last;
        }

        /// <summary>
        /// Gets the required version.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="exception">if set to <c>true</c> [exception].</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static int GetRequiredVersion(Type packet, out bool exception)
        {
            Attribute[] attributes = null;
            if (CustomAttributes.ContainsKey(packet))
                attributes = CustomAttributes[packet];
            else
            {
                lock (CustomAttributes)
                    if (!CustomAttributes.ContainsKey(packet))
                    {
                        CustomAttributes.Add(packet, attributes = Attribute.GetCustomAttributes(packet));
                    }
                    else
                    {
                        attributes = CustomAttributes[packet];
                    }
            }
            var requireVersion = (RequireVersionAttribute) attributes.FirstOrDefault(a => a is RequireVersionAttribute);
            if (requireVersion == null)
            {
                exception = false;
                return -1;
            }
            exception = requireVersion.ThrowException;
            return requireVersion.VersionNumber;
        }

        /// <summary>
        /// Gets the last supported version.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="exception">if set to <c>true</c> [exception].</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static int GetLastSupportedVersion(Type packet, out bool exception)
        {
            Attribute[] attributes = null;
            if (CustomAttributes.ContainsKey(packet))
                attributes = CustomAttributes[packet];
            else
            {
                lock (CustomAttributes)
                    CustomAttributes.Add(packet, attributes = Attribute.GetCustomAttributes(packet));
            }
            var lastSupportedVersion =
                (LastSupportedVersionAttribute) attributes.FirstOrDefault(a => a is LastSupportedVersionAttribute);
            if (lastSupportedVersion == null)
            {
                exception = false;
                return -1;
            }
            exception = lastSupportedVersion.ThrowException;
            return lastSupportedVersion.VersionNumber;
        }


        /// <summary>
        /// Sends the packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <remarks></remarks>
        [Obsolete("Use async method instead")]
        public void SendPacket(Packet packet)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");
            lock (_writeLock)
            {
                Type type = packet.GetType();
                PacketFactory.PacketSender p = PacketFactory.GetSender(type);

                p(_innerStream, Version, packet);
                if (UseCache)
                {
                    if (!PacketCache.ContainsKey(type))
                    {
                        lock (PacketCache)
                        {
                            if (!PacketCache.ContainsKey(type))
                            {
                                PacketCache.Add(type, new LockFreeQueue<Packet>());
                            }
                        }
                    }
                    if (packet.Cache)
                        PacketCache[type].Enqueue(packet);
                }
            }
        }
        public async Task SendPacketAsync(Packet packet)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");
            Type type = packet.GetType();
            PacketFactory.PacketSenderAsync p = PacketFactory.GetSenderAsync(type);

            await p(_innerStream, Version, packet);
            if (UseCache)
            {
                if (!PacketCache.ContainsKey(type))
                {
                    lock (PacketCache)
                    {
                        if (!PacketCache.ContainsKey(type))
                        {
                            PacketCache.Add(type, new LockFreeQueue<Packet>());
                        }
                    }
                }
                if (packet.Cache)
                    PacketCache[type].Enqueue(packet);
            }
        }

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        /// <remarks></remarks>
        public void Shutdown()
        {
            _innerStream.Close();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification="Its a protocol version name")]
        public bool Use12w18aFix { get; set; }
    }
}