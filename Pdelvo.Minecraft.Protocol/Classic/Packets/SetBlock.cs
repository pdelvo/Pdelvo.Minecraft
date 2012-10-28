using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{

    public class SetBlock : Protocol.Packets.Packet
    {
        public short PositionX { get; set; }
        public short PositionY { get; set; }
        public short PositionZ { get; set; }
        public byte Mode { get; set; }
        public byte BlockType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetBlock"/> class.
        /// </summary>
        /// <remarks></remarks>
        public SetBlock()
        {
            Code = 0x05;
        }

        /// <summary>
        /// Receives the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnReceive(Network.BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            PositionX = reader.ReadInt16();
            PositionY = reader.ReadInt16();
            PositionZ = reader.ReadInt16();
            Mode = reader.ReadByte();
            BlockType = reader.ReadByte();
        }

        /// <summary>
        /// Sends the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnSend(Network.BigEndianStream writer, int version)
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
