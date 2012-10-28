using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{

    public class LevelFinalize : Protocol.Packets.Packet
    {
        public short XSize { get; set; }
        public short YSize { get; set; }
        public short ZSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelFinalize"/> class.
        /// </summary>
        /// <remarks></remarks>
        public LevelFinalize()
        {
            Code = 0x04;
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

            XSize = reader.ReadInt16();
            YSize = reader.ReadInt16();
            ZSize = reader.ReadInt16();
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

            writer.Write(XSize);
            writer.Write(YSize);
            writer.Write(ZSize);
        }
    }
}
