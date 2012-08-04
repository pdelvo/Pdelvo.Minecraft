using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{

    public class LevelDataChunk : Protocol.Packets.Packet
    {
        public IEnumerable<byte> ChunkData { get; set; }
        public byte PercentComplete { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelDataChunk"/> class.
        /// </summary>
        /// <remarks></remarks>
        public LevelDataChunk()
        {
            Code = 0x02;
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

            ChunkData = reader.ReadBytes(reader.ReadInt16());
            PercentComplete = reader.ReadByte();
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

            var data = ChunkData.ToArray();

            writer.Write((short)data.Length);
            writer.Write(data);
            writer.Write(PercentComplete);
        }
    }
}
