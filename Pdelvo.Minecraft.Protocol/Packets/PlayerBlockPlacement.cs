using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class PlayerBlockPlacement : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerBlockPlacement"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PlayerBlockPlacement()
        {
            Code = 0x0F;
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
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        /// <remarks></remarks>
        public byte Direction { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        /// <remarks></remarks>
        public ItemStack Item { get; set; }

        /// <summary>
        /// The position X the player clicked on the block
        /// </summary>
        public byte BlockX { get; set; }

        /// <summary>
        /// The position Y the player clicked on the block
        /// </summary>
        public byte BlockY { get; set; }

        /// <summary>
        /// The position Z the player clicked on the block
        /// </summary>
        public byte BlockZ { get; set; }

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
            PositionX = reader.ReadInt32();
            PositionY = reader.ReadByte();
            PositionZ = reader.ReadInt32();
            Direction = reader.ReadByte();
            Item = ItemStack.Read(reader);

            if (version >= 34)
            {
                BlockX = reader.ReadByte();
                BlockY = reader.ReadByte();
                BlockZ = reader.ReadByte();
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
                throw new System.ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Direction);
            writer.Write(Item);
            if (version >= 34)
            {
                writer.Write(BlockX);
                writer.Write(BlockY);
                writer.Write(BlockZ);
            }
        }
    }
}