using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class Transaction : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Transaction()
        {
            Code = 0x6A;
        }

        /// <summary>
        /// Gets or sets the window Id.
        /// </summary>
        /// <value>The window Id.</value>
        /// <remarks></remarks>
        public byte WindowId { get; set; }
        /// <summary>
        /// Gets or sets the action number.
        /// </summary>
        /// <value>The action number.</value>
        /// <remarks></remarks>
        public short ActionNumber { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Transaction"/> is accepted.
        /// </summary>
        /// <value><c>true</c> if accepted; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool Accepted { get; set; }

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
            WindowId = reader.ReadByte();
            ActionNumber = reader.ReadInt16();
            Accepted = reader.ReadBoolean();
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
            writer.Write(WindowId);
            writer.Write(ActionNumber);
            writer.Write(Accepted);
        }
    }
}