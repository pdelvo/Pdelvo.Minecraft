using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class WindowClick : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowClick"/> class.
        /// </summary>
        /// <remarks></remarks>
        public WindowClick()
        {
            Code = 0x66;
        }

        /// <summary>
        /// Gets or sets the window Id.
        /// </summary>
        /// <value>The window Id.</value>
        /// <remarks></remarks>
        public byte WindowId { get; set; }
        /// <summary>
        /// Gets or sets the slot.
        /// </summary>
        /// <value>The slot.</value>
        /// <remarks></remarks>
        public short Slot { get; set; }
        /// <summary>
        /// Gets or sets the right click.
        /// </summary>
        /// <value>The right click.</value>
        /// <remarks></remarks>
        public byte RightClick { get; set; }
        /// <summary>
        /// Gets or sets the action number.
        /// </summary>
        /// <value>The action number.</value>
        /// <remarks></remarks>
        public short ActionNumber { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WindowClick"/> is shift.
        /// </summary>
        /// <value><c>true</c> if shift; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public byte Shift { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        /// <remarks></remarks>
        public ItemStack Item { get; set; }

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
            WindowId = reader.ReadByte();
            Slot = reader.ReadInt16();
            RightClick = reader.ReadByte();
            ActionNumber = reader.ReadInt16();
            if (version >= 45)
                Shift = reader.ReadByte();
            else
                Shift = reader.ReadBoolean() ? (byte)1 : (byte)0;
            Item = ItemStack.Read(reader);
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
            writer.Write(WindowId);
            writer.Write(Slot);
            writer.Write(RightClick);
            writer.Write(ActionNumber);
            if (version >= 45)
                writer.Write(Shift);
            else
                writer.Write(Shift != 0);
            writer.Write(Item);
        }
    }
}