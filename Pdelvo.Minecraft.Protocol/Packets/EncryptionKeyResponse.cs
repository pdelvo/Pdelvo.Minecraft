using System;
using System.Collections.Generic;
using System.Linq;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [RequireVersion(31, true)]
    [PacketUsage(PacketUsage.Both)]
    public class EncryptionKeyResponse : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="EncryptionKeyRequest" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public EncryptionKeyResponse()
        {
            Code = 0xFC;
            VerifyToken = new byte[0];
        }

        public IEnumerable<byte> SharedKey { get; set; }
        public IEnumerable<byte> VerifyToken { get; set; }

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
            SharedKey = reader.ReadBytes(reader.ReadInt16 ());
            if (version >= 37)
                VerifyToken = reader.ReadBytes(reader.ReadInt16 ());
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
            if (SharedKey == null)
                SharedKey = new byte[0];
            writer.Write((short) SharedKey.Count ());
            writer.Write(SharedKey.ToArray ());
            if (version >= 37)
            {
                writer.Write((short) VerifyToken.Count ());
                writer.Write(VerifyToken.ToArray ());
            }
        }
    }
}