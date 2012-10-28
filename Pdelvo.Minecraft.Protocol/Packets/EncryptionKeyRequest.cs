using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [RequireVersion(31, true)]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class EncryptionKeyRequest : Packet
    {
        public string ServerId { get; set; }
        public IEnumerable<byte> PublicKey { get; set; }
        public IEnumerable<byte> VerifyToken { get; set; }

           /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionKeyRequest"/> class.
        /// </summary>
        /// <remarks></remarks>
        public EncryptionKeyRequest()
        {
            Code = 0xFD;
            VerifyToken = new byte[0];
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
            ServerId = reader.ReadString16();
            PublicKey = reader.ReadBytes(reader.ReadInt16());
            if(version >=37)
            VerifyToken = reader.ReadBytes(reader.ReadInt16());
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
            writer.Write(ServerId);
            if (PublicKey == null)
                PublicKey = new byte[0];
            writer.Write((short)PublicKey.Count());
            writer.Write(PublicKey.ToArray());
            if (version >= 37)
            {
                writer.Write((short)VerifyToken.Count());
                writer.Write(VerifyToken.ToArray());
            }
        }
    }
}
