using System;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{
    public class ServerIdentification : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ServerIdentification" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public ServerIdentification()
        {
            Code = 0x00;
        }

        public byte ProtocolVersion { get; set; }
        public string Servername { get; set; }
        public string ServerMotD { get; set; }
        public byte Usertype { get; set; }

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

            ProtocolVersion = reader.ReadByte ();
            Servername = reader.ReadClassicString ();
            ServerMotD = reader.ReadClassicString ();
            Usertype = reader.ReadByte ();
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

            writer.Write(ProtocolVersion);
            writer.WriteClassicString(Servername);
            writer.WriteClassicString(ServerMotD);
            writer.Write(Usertype);
        }
    }
}