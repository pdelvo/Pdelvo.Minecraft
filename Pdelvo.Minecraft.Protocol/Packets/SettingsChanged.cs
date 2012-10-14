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
        public ChatFlags ChatOptions { get; set; }
        public byte Difficulty { get; set; }
        public bool ShowCape { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyPacket"/> class.
        /// </summary>
        /// <remarks></remarks>
        public SettingsChanged()
        {
            Code = 0xCC;

            ShowCape = true;
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
                ChatOptions = (ChatFlags)reader.ReadByte();
            if (version >= 32)
                Difficulty = reader.ReadByte();
            if (version >= 46)
                ShowCape = reader.ReadBoolean();
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
                writer.Write((byte)ChatOptions);
            if (version >= 32)
                writer.Write(Difficulty);
            if (version >= 46)
                writer.Write(ShowCape);
        }
    }

    [Flags]
    public enum ChatFlags : byte
    {
        Enabled = 0x00,
        CommandsOnly = 0x01,
        Hidden = 0x02,
        ColorsEnabled = 0x04
    }
}
