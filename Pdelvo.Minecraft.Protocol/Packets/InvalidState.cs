using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class InvalidState : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidState"/> class.
        /// </summary>
        /// <remarks></remarks>
        public InvalidState()
        {
            Code = 0x46;
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        /// <remarks></remarks>
        public byte Reason { get; set; }
        /// <summary>
        /// Gets or sets the game mode.
        /// </summary>
        /// <value>The game mode.</value>
        /// <remarks></remarks>
        public byte GameMode { get; set; }

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
            Reason = reader.ReadByte();
            GameMode = reader.ReadByte();

            //Console.WriteLine("Invalide State: Reason: " + Reason + " GameMode: " + GameMode);
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
            writer.Write(Reason);
            writer.Write(GameMode);
        }
    }
}