using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class UpdateProgressBar : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProgressBar"/> class.
        /// </summary>
        /// <remarks></remarks>
        public UpdateProgressBar()
        {
            Code = 0x69;
        }

        /// <summary>
        /// Gets or sets the window Id.
        /// </summary>
        /// <value>The window Id.</value>
        /// <remarks></remarks>
        public byte WindowId { get; set; }
        /// <summary>
        /// Gets or sets the type of the progress bar.
        /// </summary>
        /// <value>The type of the progress bar.</value>
        /// <remarks></remarks>
        public short ProgressBarType { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <remarks></remarks>
        public short Value { get; set; }

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
            ProgressBarType = reader.ReadInt16();
            Value = reader.ReadInt16();
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
            writer.Write(ProgressBarType);
            writer.Write(Value);
        }
    }
}