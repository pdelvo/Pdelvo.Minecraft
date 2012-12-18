using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.Both)]
    [LastSupportedVersion(50)]
    public class PickupSpawn : Packet, IEntityPacket
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="PickupSpawn" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public PickupSpawn()
        {
            Code = 0x15;

            Item = new ItemStack ();
        }

        /// <summary>
        ///   Gets or sets the item.
        /// </summary>
        /// <value> The item. </value>
        /// <remarks>
        /// </remarks>
        public ItemStack Item { get; set; }

        /// <summary>
        ///   Gets or sets the X.
        /// </summary>
        /// <value> The X. </value>
        /// <remarks>
        /// </remarks>
        public int PositionX { get; set; }

        /// <summary>
        ///   Gets or sets the Y.
        /// </summary>
        /// <value> The Y. </value>
        /// <remarks>
        /// </remarks>
        public int PositionY { get; set; }

        /// <summary>
        ///   Gets or sets the Z.
        /// </summary>
        /// <value> The Z. </value>
        /// <remarks>
        /// </remarks>
        public int PositionZ { get; set; }

        /// <summary>
        ///   Gets or sets the rotation.
        /// </summary>
        /// <value> The rotation. </value>
        /// <remarks>
        /// </remarks>
        public byte Rotation { get; set; }

        /// <summary>
        ///   Gets or sets the pitch.
        /// </summary>
        /// <value> The pitch. </value>
        /// <remarks>
        /// </remarks>
        public byte Pitch { get; set; }

        /// <summary>
        ///   Gets or sets the roll.
        /// </summary>
        /// <value> The roll. </value>
        /// <remarks>
        /// </remarks>
        public byte Roll { get; set; }

        #region IEntityPacket Members

        /// <summary>
        ///   Gets or sets the entity Id.
        /// </summary>
        /// <value> The entity Id. </value>
        /// <remarks>
        /// </remarks>
        public int EntityId { get; set; }

        #endregion

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
            EntityId = reader.ReadInt32 ();
            if (version >= 41)
                Item = ItemStack.Read(reader);
            else
            {
                Item = new ItemStack
                           {
                               ItemType = reader.ReadInt16 (),
                               Count = reader.ReadByte (),
                               Durability = reader.ReadInt16 (),
                               AdditionalData = new byte[0]
                           };
            }
            PositionX = reader.ReadInt32 ();
            PositionY = reader.ReadInt32 ();
            PositionZ = reader.ReadInt32 ();
            Rotation = reader.ReadByte ();
            Pitch = reader.ReadByte ();
            Roll = reader.ReadByte ();
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
            writer.Write(EntityId);
            if (version >= 41)
                writer.Write(Item);
            else
            {
                writer.Write(Item.ItemType);
                writer.Write(Item.Count);
                writer.Write(Item.Durability);
            }
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Rotation);
            writer.Write(Pitch);
            writer.Write(Roll);
        }
    }
}