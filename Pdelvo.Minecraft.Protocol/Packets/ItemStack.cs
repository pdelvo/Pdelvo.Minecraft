using System.Linq;
using Pdelvo.Minecraft.Network;
using System.Text;
using System.Collections.Generic;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class ItemStack
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        /// <remarks></remarks>
        public short ItemType { get; set; }
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        /// <remarks></remarks>
        public byte Count { get; set; }
        /// <summary>
        /// Gets or sets the durability.
        /// </summary>
        /// <value>The durability.</value>
        /// <remarks></remarks>
        public short Durability { get; set; }
        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        /// <value>The additional data.</value>
        /// <remarks></remarks>
        public IEnumerable<byte> AdditionalData { get; set; }

        static short[] _12w18a = new short[]
                                                         {
                                                             0x182, // Book & Quills
                                                             0x183, //Written Book

                                                             //ARMOUR
                                                             //helmet, chestplate, leggings, boots
                                                             0x12A, 0x12B, 0x12C, 0x12D, //LEATHER
                                                             0x12E, 0x12F, 0x130, 0x131, //CHAIN
                                                             0x132, 0x133, 0x134, 0x135, //IRON
                                                             0x136, 0x137, 0x138, 0x139, //DIAMOND
                                                             0x13A, 0x13B, 0x13C, 0x13D //GOLD
                                                         };

        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static ItemStack Read(BigEndianStream stream)
        {
            if (stream == null)
                throw new System.ArgumentNullException("stream");
            var stack = new ItemStack();
            if ((stack.ItemType = stream.ReadInt16()) >= 0)
            {
                stack.Count = stream.ReadByte();
                stack.Durability = stream.ReadInt16();
            }
            else
                return stack;
            if (((stream.Owner as PacketEndPoint).Version >=36 && stack.ItemType != -1) || (EnchantHelper.Enchantable.Contains(stack.ItemType) && !((stream.Owner as PacketEndPoint).Use12w18aFix && !(_12w18a.Contains(stack.ItemType)))))//12w18a fix
            {
                short hasData = stream.ReadInt16();
                if (hasData > 0)
                {
                    stack.AdditionalData = stream.ReadBytes(hasData);
                }
            }
            if (stack.AdditionalData == null)
                stack.AdditionalData = new byte[0];

            return stack;
        }

        /// <summary>
        /// Writes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="stack">The stack.</param>
        /// <remarks></remarks>
        public static void Write(BigEndianStream stream, ItemStack stack)
        {
            if (stream == null)
                throw new System.ArgumentNullException("stream");
            if ((stack == null))
                stack = new ItemStack { ItemType = 0 };
            stream.Write(stack.ItemType);
            if (stack.ItemType >= 0)
            {
                stream.Write(stack.Count);
                stream.Write(stack.Durability);
            }
            if (((stream.Owner as PacketEndPoint).Version >= 36 && stack.ItemType != -1) || (EnchantHelper.Enchantable.Contains(stack.ItemType) && !((stream.Owner as PacketEndPoint).Use12w18aFix && !(_12w18a.Contains(stack.ItemType)))))//12w18a fix
            {
                short hasData = stack.AdditionalData == null ? (short)0 : (short)stack.AdditionalData.Count();
                if (hasData > 0)
                {
                    stream.Write(hasData);
                    stream.Write(stack.AdditionalData.ToArray());
                }
                else
                {
                    stream.Write((short)-1);
                }
            }
        }
    }
}