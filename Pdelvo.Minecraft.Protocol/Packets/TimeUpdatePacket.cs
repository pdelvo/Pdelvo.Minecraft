using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class TimeUpdatePacket : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeUpdatePacket"/> class.
        /// </summary>
        /// <remarks></remarks>
        public TimeUpdatePacket()
        {
            Code = 0x04;
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        /// <remarks></remarks>
        public long Time { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The date.</value>
        /// <remarks></remarks>
        public long DayTime { get; set; }

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
            if (version >= 40)
                Time = reader.ReadInt64();
            DayTime = reader.ReadInt64();
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
            if (version >= 40)
                writer.Write(Time);
            writer.Write(DayTime);
        }
    }
}