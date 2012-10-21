using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ClientToServer)]
    public class PlayerLook : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerLook"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PlayerLook()
        {
            Code = 0x0C;
        }

        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>The yaw.</value>
        /// <remarks></remarks>
        public float Yaw { get; set; }
        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>The pitch.</value>
        /// <remarks></remarks>
        public float Pitch { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [on ground].
        /// </summary>
        /// <value><c>true</c> if [on ground]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool OnGround { get; set; }

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
            Yaw = reader.ReadSingle();
            Pitch = reader.ReadSingle();
            OnGround = reader.ReadBoolean();
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
            writer.Write(Yaw);
            writer.Write(Pitch);
            writer.Write(OnGround);
        }
    }
}