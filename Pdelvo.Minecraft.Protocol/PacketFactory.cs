using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Pdelvo.Minecraft.Protocol.Packets;
using Pdelvo.Minecraft.Network;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public static class PacketFactory
    {
        #region Delegates
        internal delegate Packet PacketCreator();
        internal delegate void PacketSender(BigEndianStream writer, int version, Packet packet);
        internal delegate Task PacketSenderAsync(BigEndianStream writer, int version, Packet packet);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private static readonly Dictionary<Type, PacketCreator> CreateCache = new Dictionary<Type, PacketCreator>();

        /// <summary>
        /// 
        /// </summary>
        private static readonly Dictionary<Type, PacketSender> SendCache = new Dictionary<Type, PacketSender>();
        private static readonly Dictionary<Type, PacketSenderAsync> SendCacheAsync = new Dictionary<Type, PacketSenderAsync>();

        /// <summary>
        /// Gets the creator.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static PacketCreator GetCreator(Type type)
        {
            if (CreateCache.ContainsKey(type)) return CreateCache[type];
            lock (CreateCache)
            {
                if (CreateCache.ContainsKey(type)) return CreateCache[type];
                var dyn = new DynamicMethod("PC_FACTORY_" + type.Name, typeof(Packet), new Type[] { });
                ILGenerator gen = dyn.GetILGenerator();
                gen.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { })); //Constructoraufruf
                gen.Emit(OpCodes.Ret); //Rückgabe der neuen Instanz
                var ret = (PacketCreator)dyn.CreateDelegate(typeof(PacketCreator));
                CreateCache.Add(type, ret);
                return ret;
            }
        }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static PacketSender GetSender(Type type)
        {
            if (SendCache.ContainsKey(type)) return SendCache[type];
            lock (SendCache)
            {
                if (SendCache.ContainsKey(type)) return SendCache[type];
                bool throwOnRequired;
                bool throwOnLast;

                int requiredVersion = PacketEndPoint.GetRequiredVersion(type, out throwOnRequired);
                int lastSupportedVersion = PacketEndPoint.GetLastSupportedVersion(type, out throwOnLast);

                var dyn = new DynamicMethod("PS_FACTORY_" + type.Name, typeof(void),
                                            new[] { typeof(BigEndianStream), typeof(int), typeof(Packet) });
                ILGenerator gen = dyn.GetILGenerator();
                gen.Emit(OpCodes.Ldarg_2); // Lade packet
                gen.Emit(OpCodes.Ldarg_0); // Lade parameter BigEndianStream auf den Stack
                gen.Emit(OpCodes.Ldarg_1); // Lade die Minecraft Version auf den Stack
                gen.Emit(OpCodes.Ldc_I4, requiredVersion);
                gen.Emit(OpCodes.Ldc_I4, lastSupportedVersion);
                gen.Emit(throwOnRequired ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                gen.Emit(throwOnLast ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Call,
                         typeof(PacketFactory).GetMethod("SendPacket",
                                                          BindingFlags.Static | BindingFlags.Public |
                                                          BindingFlags.InvokeMethod));
                gen.Emit(OpCodes.Ret);
                var ret = (PacketSender)dyn.CreateDelegate(typeof(PacketSender));
                SendCache.Add(type, ret);
                return ret;
            }
        }
        internal static PacketSenderAsync GetSenderAsync(Type type)
        {
            if (SendCacheAsync.ContainsKey(type)) return SendCacheAsync[type];
            lock (SendCacheAsync)
            {
                if (SendCacheAsync.ContainsKey(type)) return SendCacheAsync[type];
                bool throwOnRequired;
                bool throwOnLast;

                int requiredVersion = PacketEndPoint.GetRequiredVersion(type, out throwOnRequired);
                int lastSupportedVersion = PacketEndPoint.GetLastSupportedVersion(type, out throwOnLast);

                var dyn = new DynamicMethod("PSA_FACTORY_" + type.Name, typeof(Task),
                                            new[] { typeof(BigEndianStream), typeof(int), typeof(Packet) });
                ILGenerator gen = dyn.GetILGenerator();
                gen.Emit(OpCodes.Ldarg_2); // Lade packet
                gen.Emit(OpCodes.Ldarg_0); // Lade parameter BigEndianStream auf den Stack
                gen.Emit(OpCodes.Ldarg_1); // Lade die Minecraft Version auf den Stack
                gen.Emit(OpCodes.Ldc_I4, requiredVersion);
                gen.Emit(OpCodes.Ldc_I4, lastSupportedVersion);
                gen.Emit(throwOnRequired ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                gen.Emit(throwOnLast ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Call,
                         typeof(PacketFactory).GetMethod("SendPacketAsync",
                                                          BindingFlags.Static | BindingFlags.Public |
                                                          BindingFlags.InvokeMethod));
                gen.Emit(OpCodes.Ret); //Rückgabe des Tasks
                var ret = (PacketSenderAsync)dyn.CreateDelegate(typeof(PacketSenderAsync));
                SendCacheAsync.Add(type, ret);
                return ret;
            }
        }

        /// <summary>
        /// Sends the packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="version">The version.</param>
        /// <param name="requiredVersion">The required version.</param>
        /// <param name="lastSupportedVersion">The last supported version.</param>
        /// <param name="throwOnRequired">if set to <c>true</c> [throw on required].</param>
        /// <param name="throwOnLast">if set to <c>true</c> [throw on last].</param>
        /// <remarks></remarks>
        public static void SendPacket(Packet packet, BigEndianStream stream,
                                      int version, int requiredVersion, int lastSupportedVersion, bool throwOnRequired,
                                      bool throwOnLast)
        {
            bool supported = CheckPacket(packet, version, requiredVersion, lastSupportedVersion, throwOnRequired, throwOnLast);
            if (!supported)
                return;
            packet.SendItem(stream, version);
        }

        public static async Task SendPacketAsync(Packet packet, BigEndianStream stream,
                              int version, int requiredVersion, int lastSupportedVersion, bool throwOnRequired,
                              bool throwOnLast)
        {
            bool supported = CheckPacket(packet, version, requiredVersion, lastSupportedVersion, throwOnRequired, throwOnLast);
            if (!supported)
                return;
            await packet.SendItemAsync(stream, version);
        }

        private static bool CheckPacket(Packet packet, int version, int requiredVersion, int lastSupportedVersion, bool throwOnRequired, bool throwOnLast)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");
            bool require = (requiredVersion == -1 || requiredVersion <= version);
            bool last = (lastSupportedVersion == -1 || lastSupportedVersion >= version);
            if (!require && throwOnRequired)
                throw new ArgumentException("packet does not support minecraft version", "packet",
                                            new PacketException(packet.Code));
            if (!last && throwOnLast)
                throw new ArgumentException("packet does not support minecraft version", "packet",
                                            new PacketException(packet.Code));
            bool supported = require && last;
            return supported;
        }
    }
}