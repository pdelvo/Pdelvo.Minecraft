using System;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{
    public class LevelFinalize : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="LevelFinalize" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public LevelFinalize()
        {
            Code = 0x04;
        }

        public short XSize { get; set; }
        public short YSize { get; set; }
        public short ZSize { get; set; }

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

            XSize = reader.ReadInt16 ();
            YSize = reader.ReadInt16 ();
            ZSize = reader.ReadInt16 ();
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

            writer.Write(XSize);
            writer.Write(YSize);
            writer.Write(ZSize);
        }
    }
}