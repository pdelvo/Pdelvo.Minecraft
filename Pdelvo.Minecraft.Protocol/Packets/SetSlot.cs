using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class SetSlot : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetSlot"/> class.
        /// </summary>
        /// <remarks></remarks>
        public SetSlot()
        {
            Code = 0x67;
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
            writer.Write(Item);
        }
    }
}