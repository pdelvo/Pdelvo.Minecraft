using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [LastSupportedVersion(31)]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class PreChunk : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreChunk"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PreChunk()
        {
            Code = 0x32;
        }

        public override bool CanBeDelayed
        {
            get
            {
                return true;
            }
        } 

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        /// <remarks></remarks>
        public int PositionX { get; set; }
        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        /// <remarks></remarks>
        public int PositionZ { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PreChunk"/> is mode.
        /// </summary>
        /// <value><c>true</c> if mode; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool Mode { get; set; }

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
            PositionX = reader.ReadInt32();
            PositionZ = reader.ReadInt32();
            Mode = reader.ReadBoolean();
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
            writer.Write(PositionX);
            writer.Write(PositionZ);
            writer.Write(Mode);
        }
    }
}