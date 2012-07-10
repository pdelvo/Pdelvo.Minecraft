using System.Collections.Generic;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public static class InventoryPacket
    {
        /// <summary>
        /// Parses the payload.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<ItemStack> ParsePayload(BigEndianStream reader, short count)
        {
            if (reader == null)
                throw new System.ArgumentNullException("reader");
            var items = new List<ItemStack>();
            for (int i = 0; i < count; i++)
            {
                ItemStack item = ItemStack.Read(reader);
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// Writes the payload.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="items">The items.</param>
        /// <remarks></remarks>
        public static void WritePayload(BigEndianStream writer, IEnumerable<ItemStack> items)
        {
            if (writer == null)
                throw new System.ArgumentNullException("writer");
            if (items == null) items = new List<ItemStack>();
            foreach (ItemStack item in items)
            {
                ItemStack.Write(writer, item);
            }
        }
    }
}