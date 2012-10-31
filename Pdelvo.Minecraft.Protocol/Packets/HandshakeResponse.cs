using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [Obsolete("In the newest protocol version this packet is not longer used")]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class HandshakeResponse : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="HandshakeResponse" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public HandshakeResponse()
        {
            Code = 2;
        }

        /// <summary>
        ///   Gets or sets the hash.
        /// </summary>
        /// <value> The hash. </value>
        /// <remarks>
        /// </remarks>
        public string Hash { get; set; }

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
            Hash = reader.ReadString16 ();
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
            writer.Write(Hash);
        }
    }
}