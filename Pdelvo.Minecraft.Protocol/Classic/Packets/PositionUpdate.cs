using System;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{
    public class PositionUpdate : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="PositionUpdate" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public PositionUpdate()
        {
            Code = 0x0A;
        }

        public byte PlayerID { get; set; }
        public byte ChangeX { get; set; }
        public byte ChangeY { get; set; }
        public byte ChangeZ { get; set; }

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
            PlayerID = reader.ReadByte ();
            ChangeX = reader.ReadByte ();
            ChangeY = reader.ReadByte ();
            ChangeZ = reader.ReadByte ();
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

            writer.Write(PlayerID);
            writer.Write(ChangeX);
            writer.Write(ChangeY);
            writer.Write(ChangeZ);
        }
    }
}