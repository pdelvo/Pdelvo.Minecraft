using Pdelvo.Minecraft.Protocol.Packets;
using Pdelvo.Minecraft.Network;
using System;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public static class NetworkExtensions
    {
        /// <summary>
        /// Writes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="data">The data.</param>
        /// <remarks></remarks>
        public static void Write(this BigEndianStream stream, EntityInformation data)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (data == null)
                throw new ArgumentNullException("data");
            stream.Write(data.Data1);
            stream.Write(data.Data2);
            stream.Write(data.Data3);
        }

        /// <summary>
        /// Writes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="data">The data.</param>
        /// <remarks></remarks>
        public static void Write(this BigEndianStream stream, ItemStack data)
        {
            ItemStack.Write(stream, data);
        }

        /// <summary>
        /// Writes the packet.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        public static void WritePacket(this BigEndianStream stream, Packet packet, int version)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");
            packet.SendItem(stream, version);
        }

        /// <summary>
        /// Reads the meta data.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static MetadataInfo ReadMetaData(this BigEndianStream stream, int version)
        {
            return MetadataInfo.Read(stream, version);
        }

        /// <summary>
        /// Writes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="data">The data.</param>
        /// <remarks></remarks>
        public static void Write(this BigEndianStream stream, MetadataInfo data)
        {
            MetadataInfo.WriteMetadata(data, stream);
        }
    }
}