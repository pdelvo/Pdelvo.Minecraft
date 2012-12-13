using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [RequireVersion(36)]
    [PacketUsage(PacketUsage.ClientToServer)]
    public class RespawnRequestPacket : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="EmptyPacket" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public RespawnRequestPacket()
        {
            Code = 0xCD;
        }

        public byte ResponseType { get; set; }


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
            ResponseType = reader.ReadByte ();
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
            writer.Write(ResponseType);
        }
    }
}