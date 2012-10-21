using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class EntityLookAndRelativeMove : Packet, IEntityPacket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLookAndRelativeMove"/> class.
        /// </summary>
        /// <remarks></remarks>
        public EntityLookAndRelativeMove()
        {
            Code = 0x21;
        }

        /// <summary>
        /// Gets or sets the entity Id.
        /// </summary>
        /// <value>The entity Id.</value>
        /// <remarks></remarks>
        public int EntityId { get; set; }
        /// <summary>
        /// Gets or sets the offset X.
        /// </summary>
        /// <value>The offset X.</value>
        /// <remarks></remarks>
        public byte OffsetX { get; set; }
        /// <summary>
        /// Gets or sets the offset Y.
        /// </summary>
        /// <value>The offset Y.</value>
        /// <remarks></remarks>
        public byte OffsetY { get; set; }
        /// <summary>
        /// Gets or sets the offset Z.
        /// </summary>
        /// <value>The offset Z.</value>
        /// <remarks></remarks>
        public byte OffsetZ { get; set; }
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
        /// Receives the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            EntityId = reader.ReadInt32();
            OffsetX = reader.ReadByte();
            OffsetY = reader.ReadByte();
            OffsetZ = reader.ReadByte();
            Yaw = reader.ReadByte();
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
                throw new ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(EntityId);
            writer.Write(OffsetX);
            writer.Write(OffsetY);
            writer.Write(OffsetZ);
            writer.Write(Yaw);
            writer.Write(Pitch);
        }
    }
}