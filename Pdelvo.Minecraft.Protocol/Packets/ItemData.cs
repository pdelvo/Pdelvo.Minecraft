using System;
using System.Collections.Generic;
using System.Linq;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class ItemData : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ItemData" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public ItemData()
        {
            Code = 0x83;

            InnerData = Enumerable.Empty<byte> ();
        }

        /// <summary>
        ///   Gets or sets the type of the item.
        /// </summary>
        /// <value> The type of the item. </value>
        /// <remarks>
        /// </remarks>
        public short ItemType { get; set; }

        /// <summary>
        ///   Gets or sets the item Id.
        /// </summary>
        /// <value> The item Id. </value>
        /// <remarks>
        /// </remarks>
        public short ItemId { get; set; }

        /// <summary>
        ///   Gets or sets the inner data.
        /// </summary>
        /// <value> The inner data. </value>
        /// <remarks>
        /// </remarks>
        public IEnumerable<byte> InnerData { get; set; }

        /// <summary>
        ///   Receives the specified reader.
        /// </summary>
        /// <param name="reader"> The reader. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            ItemType = reader.ReadInt16 ();
            ItemId = reader.ReadInt16 ();
            InnerData = reader.ReadBytes(version >= 49 ? reader.ReadInt16() : reader.ReadByte());
        }

        /// <summary>
        ///   Sends the specified writer.
        /// </summary>
        /// <param name="writer"> The writer. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        protected override void OnSend(BigEndianStream writer, int version)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(ItemType);
            writer.Write(ItemId);
            if (version >= 49)
                if (InnerData == null)
                    writer.Write((short)0);
                else
                    writer.Write((short)InnerData.Count());
            else
                if (InnerData == null)
                    writer.Write((byte)0);
                else
                    writer.Write((byte)InnerData.Count());
            writer.Write(InnerData.ToArray ());
        }
    }
}