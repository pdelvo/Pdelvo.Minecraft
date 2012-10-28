using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{

    public class ServerIdentification : Protocol.Packets.Packet
    {
        public byte ProtocolVersion { get; set; }
        public string Servername { get; set; }
        public string ServerMotD { get; set; }
        public byte Usertype { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerIdentification"/> class.
        /// </summary>
        /// <remarks></remarks>
        public ServerIdentification()
        {
            Code = 0x00;
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

            ProtocolVersion = reader.ReadByte();
            Servername      = reader.ReadClassicString();
            ServerMotD      = reader.ReadClassicString();
            Usertype        = reader.ReadByte();
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

            writer.Write(ProtocolVersion);
            writer.WriteClassicString(Servername);
            writer.WriteClassicString(ServerMotD);
            writer.Write(Usertype);
        }
    }
}
