using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class Respawn : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Respawn"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Respawn()
        {
            Code = 0x09;
            Generator = "DEFAULT";
        }

        public override bool CanBeDelayed
        {
            get
            {
                return true;
            }
        } 

        /// <summary>
        /// Gets or sets the world.
        /// </summary>
        /// <value>The world.</value>
        /// <remarks></remarks>
        public int World { get; set; }
        /// <summary>
        /// Gets or sets the difficulty.
        /// </summary>
        /// <value>The difficulty.</value>
        /// <remarks></remarks>
        public byte Difficulty { get; set; }
        /// <summary>
        /// Gets or sets the creative mode.
        /// </summary>
        /// <value>The creative mode.</value>
        /// <remarks></remarks>
        public byte CreativeMode { get; set; }
        /// <summary>
        /// Gets or sets the height of the world.
        /// </summary>
        /// <value>The height of the world.</value>
        /// <remarks></remarks>
        public short WorldHeight { get; set; }
        /// <summary>
        /// Gets or sets the map seed.
        /// </summary>
        /// <value>The map seed.</value>
        /// <remarks></remarks>
        public long MapSeed { get; set; }
        /// <summary>
        /// Gets or sets the generator.
        /// </summary>
        /// <value>The generator.</value>
        /// <remarks></remarks>
        public string Generator { get; set; }

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
            if (version >= 32 && reader.Context == PacketContext.Client) return;
            if (version >= 27)
                World = reader.ReadInt32();
            else
                World = reader.ReadByte();
            Difficulty = reader.ReadByte();
            CreativeMode = reader.ReadByte();
            WorldHeight = reader.ReadInt16();
            if (version <= 27)
                MapSeed = reader.ReadInt64();
            if (version >= 23)
                Generator = reader.ReadString16();
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
            if (version >= 32 && writer.Context == PacketContext.Server) return;
            if (version >= 27)
                writer.Write(World);
            else
                writer.Write((byte) World);
            writer.Write(Difficulty);
            writer.Write(CreativeMode);
            writer.Write(WorldHeight);
            if (version <= 27)
                writer.Write(MapSeed);
            if (version >= 23)
                writer.Write(Generator);
        }
    }
}