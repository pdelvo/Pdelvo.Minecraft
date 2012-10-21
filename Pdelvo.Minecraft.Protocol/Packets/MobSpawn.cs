using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class MobSpawn : Packet, IEntityPacket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MobSpawn"/> class.
        /// </summary>
        /// <remarks></remarks>
        public MobSpawn()
        {
            Code = 0x18;
        }

        /// <summary>
        /// Gets or sets the entity Id.
        /// </summary>
        /// <value>The entity Id.</value>
        /// <remarks></remarks>
        public int EntityId { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        /// <remarks></remarks>
        public byte MobType { get; set; }
        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        /// <remarks></remarks>
        public int PositionX { get; set; }
        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        /// <remarks></remarks>
        public int PositionY { get; set; }
        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        /// <remarks></remarks>
        public int PositionZ { get; set; }
        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>The yaw.</value>
        /// <remarks></remarks>
        public byte Yaw { get; set; }
        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>The pitch.</value>
        /// <remarks></remarks>
        public byte Pitch { get; set; }
        /// <summary>
        /// Gets or sets the head yaw.
        /// </summary>
        /// <value>The head yaw.</value>
        /// <remarks></remarks>
        public byte HeadYaw { get; set; }
        /// <summary>
        /// Gets or sets the data stream.
        /// </summary>
        /// <value>The data stream.</value>
        /// <remarks></remarks>
        public MetadataInfo DataStream { get; set; }

        public short Unknown1 { get; set; }
        public short Unknown2 { get; set; }
        public short Unknown3 { get; set; }

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
            MobType = reader.ReadByte();
            PositionX = reader.ReadInt32();
            PositionY = reader.ReadInt32();
            PositionZ = reader.ReadInt32();
            Yaw = reader.ReadByte();
            Pitch = reader.ReadByte();
            if (version >= 24)
                HeadYaw = reader.ReadByte();
            if (version >= 38)
            {
                Unknown1 = reader.ReadInt16();
                Unknown2 = reader.ReadInt16();
                Unknown3 = reader.ReadInt16();
            }
            DataStream = MetadataInfo.Read(reader, version);
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
            writer.Write(MobType);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Yaw);
            writer.Write(Pitch);
            if (version >= 24)
                writer.Write(HeadYaw);
            if (version >= 38)
            {
                writer.Write(Unknown1);
                writer.Write(Unknown2);
                writer.Write(Unknown3);
            }
            MetadataInfo.WriteMetadata(DataStream, writer);
        }
    }
}