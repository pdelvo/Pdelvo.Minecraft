using Pdelvo.Minecraft.Network;
using System;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class CreativeInventoryAction : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreativeInventoryAction"/> class.
        /// </summary>
        /// <remarks></remarks>
        public CreativeInventoryAction()
        {
            Code = 0x6B;
        }

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
                throw new ArgumentNullException("reader");
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
                throw new ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(Slot);
            writer.Write(Item);
        }
    }
}