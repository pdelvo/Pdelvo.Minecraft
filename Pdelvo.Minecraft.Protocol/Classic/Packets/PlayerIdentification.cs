using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{
    public class PlayerIdentification : Packet
    {
        public byte ProtocolVersion { get; set; }
        public string Username { get; set; }
        public string VerificationKey { get; set; }
        public byte Unused { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerIdentification"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PlayerIdentification()
        {
            Code = 0x00;
        }

        /// <summary>
        /// Receives the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            ProtocolVersion = reader.ReadByte();
            Username = reader.ReadClassicString();
            VerificationKey = reader.ReadClassicString();
            Unused = reader.ReadByte();
        }

        /// <summary>
        /// Sends the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnSend(BigEndianStream writer, int version)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(ProtocolVersion);
            writer.WriteClassicString(Username);
            writer.WriteClassicString(VerificationKey);
            writer.Write(Unused);
        }
    }
}
