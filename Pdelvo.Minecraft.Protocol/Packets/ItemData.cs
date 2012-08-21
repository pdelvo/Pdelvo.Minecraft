using System.Collections.Generic;
using Pdelvo.Minecraft.Network;
using System.Linq;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class ItemData : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemData"/> class.
        /// </summary>
        /// <remarks></remarks>
        public ItemData()
        {
            Code = 0x83;

            InnerData = Enumerable.Empty<byte>();
        }

        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        /// <value>The type of the item.</value>
        /// <remarks></remarks>
        public short ItemType { get; set; }
        /// <summary>
        /// Gets or sets the item Id.
        /// </summary>
        /// <value>The item Id.</value>
        /// <remarks></remarks>
        public short ItemId { get; set; }
        /// <summary>
        /// Gets or sets the inner data.
        /// </summary>
        /// <value>The inner data.</value>
        /// <remarks></remarks>
        public IEnumerable<byte> InnerData { get; set; }

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
            ItemType = reader.ReadInt16();
            ItemId = reader.ReadInt16();
            InnerData = reader.ReadBytes(reader.ReadByte());
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
            writer.Write(ItemType);
            writer.Write(ItemId);
            if (InnerData == null)
                writer.Write((byte)0);
            else
                writer.Write((byte)InnerData.Count());
            writer.Write(InnerData.ToArray());
        }
    }
}