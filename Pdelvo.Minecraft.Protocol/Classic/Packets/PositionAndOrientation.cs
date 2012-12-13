using System;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{
    public class PositionAndOrientation : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="PositionAndOrientation" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public PositionAndOrientation()
        {
            Code = 0x08;
        }

        public byte PlayerID { get; set; }
        public short PositionX { get; set; }
        public short PositionY { get; set; }
        public short PositionZ { get; set; }
        public byte Yaw { get; set; }
        public byte Pitch { get; set; }

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
            PositionX = reader.ReadInt16 ();
            PositionY = reader.ReadInt16 ();
            PositionZ = reader.ReadInt16 ();
            Yaw = reader.ReadByte ();
            Pitch = reader.ReadByte ();
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Yaw);
            writer.Write(Pitch);
        }
    }
}