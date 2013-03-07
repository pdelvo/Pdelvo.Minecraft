using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [PacketUsage(PacketUsage.ServerToClient)]
    public class DisplayScoreboard : Packet
    {
        public byte Position { get; set; }
        public string ScoreName { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DisplayScoreboard" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public DisplayScoreboard()
        {
            Code = 0xCF;
        }

        /// <summary>
        ///   Receives the specified reader.
        /// </summary>
        /// <param name="reader"> The reader. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            Position = reader.ReadByte();
            ScoreName = reader.ReadString16();
        }

        /// <summary>
        ///   Sends the specified writer.
        /// </summary>
        /// <param name="writer"> The writer. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        protected override void OnSend(BigEndianStream writer, int version)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            writer.Write(Code);

            writer.Write(Position);
            writer.Write(ScoreName);
        }
    }
}
