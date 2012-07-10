using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [RequireVersion(30)]
    public class SettingsChanged : Packet 
    {
        public string Language { get; set; }
        public int ViewDistance { get; set; }
        public byte Unknown { get; set; }
        public byte Unknown2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyPacket"/> class.
        /// </summary>
        /// <remarks></remarks>
        public SettingsChanged()
        {
            Code = 0xCC;
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
                throw new System.ArgumentNullException("reader");
            Language = reader.ReadString16();
            if (version >= 32)
                ViewDistance = reader.ReadByte();
            else
                ViewDistance = reader.ReadInt32();
            if (version >= 31)
                Unknown = reader.ReadByte();
            if (version >= 32)
                Unknown2 = reader.ReadByte();
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
                throw new System.ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(Language);
            if (version >= 32)
                writer.Write((byte)ViewDistance);
            else
                writer.Write(ViewDistance);
            if (version >= 31)
                writer.Write(Unknown);
            if (version >= 32)
                writer.Write(Unknown2);
        }
    }
}
