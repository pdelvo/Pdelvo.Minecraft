using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [RequireVersion(32)]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class NamedSoundEffect : Packet
    {
        public string Name { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public float Volume { get; set; }
        public byte Pitch { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyPacket"/> class.
        /// </summary>
        /// <remarks></remarks>
        public NamedSoundEffect()
        {
            Code = 0x3E;
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
            Name = reader.ReadString16();
            PositionX = reader.ReadInt32();
            PositionY = reader.ReadInt32();
            PositionZ = reader.ReadInt32();
            if (version >= 37)
                Volume = reader.ReadSingle();
            else
                Volume = reader.ReadByte();
            Pitch = reader.ReadByte();
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
            writer.Write(Name);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            if (version >= 37)
                writer.Write(Volume);
            else
                writer.Write((byte)Volume);
            writer.Write(Pitch);
        }
    }
}