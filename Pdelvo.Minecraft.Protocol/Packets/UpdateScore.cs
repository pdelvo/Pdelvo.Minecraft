using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [PacketUsage(PacketUsage.ServerToClient)]
    public class UpdateScore : Packet
    {
        public string ItemName { get; set; }
        public byte UpdateRemove { get; set; }
        public string ScoreName { get; set; }
        public int Value { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="UpdateScore" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public UpdateScore()
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

            ItemName = reader.ReadString16();
            UpdateRemove = reader.ReadByte();
            ScoreName = reader.ReadString16();
            Value = reader.ReadInt32();
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

            writer.Write(ItemName);
            writer.Write(UpdateRemove);
            writer.Write(ScoreName);
            writer.Write(Value);
        }
    }
}
