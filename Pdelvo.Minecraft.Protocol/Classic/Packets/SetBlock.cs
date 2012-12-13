using System;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{
    public class SetBlock : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="SetBlock" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public SetBlock()
        {
            Code = 0x05;
        }

        public short PositionX { get; set; }
        public short PositionY { get; set; }
        public short PositionZ { get; set; }
        public byte Mode { get; set; }
        public byte BlockType { get; set; }

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
            PositionX = reader.ReadInt16 ();
            PositionY = reader.ReadInt16 ();
            PositionZ = reader.ReadInt16 ();
            Mode = reader.ReadByte ();
            BlockType = reader.ReadByte ();
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Mode);
            writer.Write(BlockType);
        }
    }
}