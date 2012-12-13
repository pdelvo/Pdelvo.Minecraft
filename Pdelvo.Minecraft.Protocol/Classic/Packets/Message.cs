using System;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic.Packets
{
    public class Message : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public Message()
        {
            Code = 0x0D;
        }

        public byte MessageColor { get; set; }
        public string TextMessage { get; set; }

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

            MessageColor = reader.ReadByte ();
            TextMessage = reader.ReadClassicString ();
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

            writer.Write(MessageColor);
            writer.WriteClassicString(TextMessage);
        }
    }
}