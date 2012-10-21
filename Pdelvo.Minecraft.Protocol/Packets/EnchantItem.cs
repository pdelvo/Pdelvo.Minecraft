using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    //Added in 20
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class EnchantItem : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnchantItem"/> class.
        /// </summary>
        /// <remarks></remarks>
        public EnchantItem()
        {
            Code = 0x6C;
        }

        /// <summary>
        /// Gets or sets the window Id.
        /// </summary>
        /// <value>The window Id.</value>
        /// <remarks></remarks>
        public byte WindowId { get; set; }
        /// <summary>
        /// Gets or sets the enchantment.
        /// </summary>
        /// <value>The enchantment.</value>
        /// <remarks></remarks>
        public byte Enchantment { get; set; }

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
            WindowId = reader.ReadByte();
            Enchantment = reader.ReadByte();
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
            writer.Write(WindowId);
            writer.Write(Enchantment);
        }
    }
}