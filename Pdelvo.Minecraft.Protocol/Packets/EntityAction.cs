using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.Both)]
    public class EntityAction : Packet, IEntityPacket
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityAction" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public EntityAction()
        {
            Code = 0x13;
        }

        /// <summary>
        ///   Gets or sets the action Id.
        /// </summary>
        /// <value> The action Id. </value>
        /// <remarks>
        /// </remarks>
        public byte ActionId
        {
            get;
            set;
        }

        public int Unknown { get; set; }

        #region IEntityPacket Members

        /// <summary>
        ///   Gets or sets the entity Id.
        /// </summary>
        /// <value> The entity Id. </value>
        /// <remarks>
        /// </remarks>
        public int EntityId
        {
            get;
            set;
        }

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
            EntityId = reader.ReadInt32();
            ActionId = reader.ReadByte();
            if (version >= 72)
                Unknown = reader.ReadInt32();
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
            writer.Write(ActionId);
            if (version >= 72)
                writer.Write(Unknown);
        }
    }
}