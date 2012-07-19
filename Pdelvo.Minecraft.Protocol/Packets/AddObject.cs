using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class AddObject : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddObject"/> class.
        /// </summary>
        /// <remarks></remarks>
        public AddObject()
        {
            Code = 0x17;
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
        public byte ObjectType { get; set; }
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
        /// Gets or sets the fireball thrower.
        /// </summary>
        /// <value>Aditional Object Data.</value>
        /// <remarks></remarks>
        public int ObjectData { get; set; }
        /// <summary>
        /// Gets or sets the unknown X.
        /// </summary>
        /// <value>The unknown X.</value>
        /// <remarks></remarks>
        public short UnknownX { get; set; }
        /// <summary>
        /// Gets or sets the unknown Y.
        /// </summary>
        /// <value>The unknown Y.</value>
        /// <remarks></remarks>
        public short UnknownY { get; set; }
        /// <summary>
        /// Gets or sets the unknown Z.
        /// </summary>
        /// <value>The unknown Z.</value>
        /// <remarks></remarks>
        public short UnknownZ { get; set; }

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
            ObjectType = reader.ReadByte();
            PositionX = reader.ReadInt32();
            PositionY = reader.ReadInt32();
            PositionZ = reader.ReadInt32();
            if ((ObjectData = reader.ReadInt32()) > 0)
            {
                UnknownX = reader.ReadInt16();
                UnknownY = reader.ReadInt16();
                UnknownZ = reader.ReadInt16();
            }
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
            writer.Write(ObjectType);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(ObjectData);
            if (ObjectData > 0)
            {
                writer.Write(UnknownX);
                writer.Write(UnknownY);
                writer.Write(UnknownZ);
            }
        }
    }
}