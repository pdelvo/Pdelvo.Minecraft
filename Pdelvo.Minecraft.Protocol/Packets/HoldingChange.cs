using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class HoldingChange : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HoldingChange"/> class.
        /// </summary>
        /// <remarks></remarks>
        public HoldingChange()
        {
            Code = 0x10;
        }

        /// <summary>
        /// Gets or sets the slot Id.
        /// </summary>
        /// <value>The slot Id.</value>
        /// <remarks></remarks>
        public short SlotId { get; set; }

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
            SlotId = reader.ReadInt16();
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
            writer.Write(SlotId);
        }
    }
}