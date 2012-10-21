using System.Collections.Generic;
using Pdelvo.Minecraft.Network;
using System.Linq;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [RequireVersion(23)]
    [PacketUsage(PacketUsage.Both)]
    public class PluginMessage : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginMessage"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PluginMessage()
        {
            Code = 0xFA;
        }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        /// <remarks></remarks>
        public string Channel { get; set; }
        /// <summary>
        /// Gets or sets the internal data.
        /// </summary>
        /// <value>The internal data.</value>
        /// <remarks></remarks>
        public IEnumerable<byte> InternalData { get; set; }

        /// <summary>
        /// Receives the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new System.ArgumentNullException("reader");
            Channel = reader.ReadString16();
            InternalData = reader.ReadBytes(reader.ReadInt16());
        }

        /// <summary>
        /// Sends the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnSend(BigEndianStream writer, int version)
        {
            if (writer == null)
                throw new System.ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(Channel);
            if (InternalData != null)
            {
                writer.Write((short) InternalData.Count());
                writer.Write(InternalData.ToArray());
            }
            else
            {
                writer.Write((short) 0);
            }
        }
    }
}