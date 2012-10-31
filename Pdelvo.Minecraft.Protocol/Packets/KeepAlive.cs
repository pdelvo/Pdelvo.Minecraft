using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.Both)]
    public class KeepAlive : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="KeepAlive" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public KeepAlive()
        {
            Code = 0;
        }

        /// <summary>
        ///   Gets or sets the number.
        /// </summary>
        /// <value> The number. </value>
        /// <remarks>
        /// </remarks>
        public int Number { get; set; }

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
            Number = reader.ReadInt32 ();
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
            writer.Write(Number);
        }
    }
}