using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{

    public class PositionUpdate : Protocol.Packets.Packet
    {
        public byte PlayerID { get; set; }
        public byte ChangeX { get; set; }
        public byte ChangeY { get; set; }
        public byte ChangeZ { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionUpdate"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PositionUpdate()
        {
            Code = 0x0A;
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
            PlayerID = reader.ReadByte();
            ChangeX = reader.ReadByte();
            ChangeY = reader.ReadByte();
            ChangeZ = reader.ReadByte();
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

            writer.Write(PlayerID);
            writer.Write(ChangeX);
            writer.Write(ChangeY);
            writer.Write(ChangeZ);
        }
    }
}
