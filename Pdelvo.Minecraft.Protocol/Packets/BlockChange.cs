using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class BlockChange : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockChange"/> class.
        /// </summary>
        /// <remarks></remarks>
        public BlockChange()
        {
            Code = 0x35;
        }

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
        public byte PositionY { get; set; }
        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        /// <remarks></remarks>
        public int PositionZ { get; set; }
        /// <summary>
        /// Gets or sets the type of the block.
        /// </summary>
        /// <value>The type of the block.</value>
        /// <remarks></remarks>
        public short BlockType { get; set; }
        /// <summary>
        /// Gets or sets the block metadata.
        /// </summary>
        /// <value>The block metadata.</value>
        /// <remarks></remarks>
        public byte BlockMetadata { get; set; }

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
            PositionX = reader.ReadInt32();
            PositionY = reader.ReadByte();
            PositionZ = reader.ReadInt32();
            if (version >= 38)
                BlockType = reader.ReadInt16();
            else
                BlockType = reader.ReadByte();
            BlockMetadata = reader.ReadByte();
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            if (version >= 38)
                writer.Write(BlockType);
            else
                writer.Write((byte)BlockType);
            writer.Write(BlockMetadata);
        }
    }
}