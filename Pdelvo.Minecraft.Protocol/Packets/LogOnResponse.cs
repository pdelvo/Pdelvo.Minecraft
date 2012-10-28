using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class LogOnResponse : Packet, IEntityPacket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogOnResponse"/> class.
        /// </summary>
        /// <remarks></remarks>
        public LogOnResponse()
        {
            Code = 1;
            Generator = "DEFAULT";
        }

        /// <summary>
        /// Gets or sets the entity Id.
        /// </summary>
        /// <value>The entity Id.</value>
        /// <remarks></remarks>
        public int EntityId { get; set; }
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
        /// Gets or sets the server mode.
        /// </summary>
        /// <value>The server mode.</value>
        /// <remarks></remarks>
        public int ServerMode { get; set; }
        /// <summary>
        /// Gets or sets the dimension.
        /// </summary>
        /// <value>The dimension.</value>
        /// <remarks></remarks>
        public int Dimension { get; set; }
        /// <summary>
        /// Gets or sets the difficulty.
        /// </summary>
        /// <value>The difficulty.</value>
        /// <remarks></remarks>
        public byte Difficulty { get; set; }
        /// <summary>
        /// Gets or sets the height of the world.
        /// </summary>
        /// <value>The height of the world.</value>
        /// <remarks></remarks>
        public int WorldHeight { get; set; }
        /// <summary>
        /// Gets or sets the max players.
        /// </summary>
        /// <value>The max players.</value>
        /// <remarks></remarks>
        public byte MaxPlayers { get; set; }

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
            EntityId = reader.ReadInt32();
            reader.ReadString16(); //not used
            if (version <= 27)
                MapSeed = reader.ReadInt64();
            if (version >= 23 && version <= 30)
                Generator = reader.ReadString16();
            if (version >= 32)
                ServerMode = reader.ReadByte();
            else
                ServerMode = reader.ReadInt32();
            if (version >= 32)
                Dimension = reader.ReadByte();
            else
                Dimension = reader.ReadInt32();
            Difficulty = reader.ReadByte();
            WorldHeight = reader.ReadByte();
            MaxPlayers = reader.ReadByte();
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
            writer.Write(EntityId);
            writer.Write(""); //not used
            if (version <= 27)
                writer.Write(MapSeed);
            if (version >= 23 && version <= 30)
                writer.Write(Generator);
            if (version >= 32)
                writer.Write((byte)ServerMode);
            else
                writer.Write(ServerMode);
            if (version >= 32)
                writer.Write((byte)Dimension);
            else
                writer.Write(Dimension);
            writer.Write(Difficulty);
            writer.Write((byte)WorldHeight);
            writer.Write(MaxPlayers);
        }
    }
}