using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class CollectItem : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectItem"/> class.
        /// </summary>
        /// <remarks></remarks>
        public CollectItem()
        {
            Code = 0x16;
        }

        /// <summary>
        /// Gets or sets the collected Id.
        /// </summary>
        /// <value>The collected Id.</value>
        /// <remarks></remarks>
        public int CollectedId { get; set; }
        /// <summary>
        /// Gets or sets the collector Id.
        /// </summary>
        /// <value>The collector Id.</value>
        /// <remarks></remarks>
        public int CollectorId { get; set; }

        /// <summary>
        /// Receives the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            CollectedId = reader.ReadInt32();
            CollectorId = reader.ReadInt32();
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
                throw new ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(CollectedId);
            writer.Write(CollectorId);
        }
    }
}