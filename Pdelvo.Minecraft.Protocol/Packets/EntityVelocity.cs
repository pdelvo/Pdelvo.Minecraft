using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class EntityVelocity : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityVelocity"/> class.
        /// </summary>
        /// <remarks></remarks>
        public EntityVelocity()
        {
            Code = 0x1C;
        }

        /// <summary>
        /// Gets or sets the entity Id.
        /// </summary>
        /// <value>The entity Id.</value>
        /// <remarks></remarks>
        public int EntityId { get; set; }
        /// <summary>
        /// Gets or sets the velocity X.
        /// </summary>
        /// <value>The velocity X.</value>
        /// <remarks></remarks>
        public short VelocityX { get; set; }
        /// <summary>
        /// Gets or sets the velocity Y.
        /// </summary>
        /// <value>The velocity Y.</value>
        /// <remarks></remarks>
        public short VelocityY { get; set; }
        /// <summary>
        /// Gets or sets the velocity Z.
        /// </summary>
        /// <value>The velocity Z.</value>
        /// <remarks></remarks>
        public short VelocityZ { get; set; }

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
            VelocityX = reader.ReadInt16();
            VelocityY = reader.ReadInt16();
            VelocityZ = reader.ReadInt16();
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
            writer.Write(VelocityX);
            writer.Write(VelocityY);
            writer.Write(VelocityZ);
        }
    }
}