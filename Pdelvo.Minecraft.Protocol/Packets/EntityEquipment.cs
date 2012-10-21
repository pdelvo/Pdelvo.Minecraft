using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class EntityEquipment : Packet, IEntityPacket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEquipment"/> class.
        /// </summary>
        /// <remarks></remarks>
        public EntityEquipment()
        {
            Code = 0x05;

            Stack = new ItemStack();
        }

        /// <summary>
        /// Gets or sets the entity Id.
        /// </summary>
        /// <value>The entity Id.</value>
        /// <remarks></remarks>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the slot.
        /// </summary>
        /// <value>The slot.</value>
        /// <remarks></remarks>
        public short Slot { get; set; }

        public ItemStack Stack { get; set; }
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
            Slot = reader.ReadInt16();
            if (version >= 35)
            {
                Stack = ItemStack.Read(reader);
            }
            else
            {
                Stack = new ItemStack
                {
                    ItemType = reader.ReadInt16(),
                    Durability = reader.ReadInt16(),
                    AdditionalData = new byte[0],
                    Count = 1
                };
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
            writer.Write(Slot);
            if (version >= 35)
            {
                ItemStack.Write(writer, Stack);
            }
            else
            {
                writer.Write(Stack.ItemType);
                writer.Write(Stack.Durability);
            }
        }
    }
}