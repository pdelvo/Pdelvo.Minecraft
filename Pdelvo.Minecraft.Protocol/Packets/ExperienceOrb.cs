using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class ExperienceOrb : Packet, IEntityPacket
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ExperienceOrb" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public ExperienceOrb()
        {
            Code = 0x1A;
        }

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
        ///   Gets or sets the count.
        /// </summary>
        /// <value> The count. </value>
        /// <remarks>
        /// </remarks>
        public short Count { get; set; }

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
            PositionX = reader.ReadInt32 ();
            PositionY = reader.ReadInt32 ();
            PositionZ = reader.ReadInt32 ();
            Count = reader.ReadInt16 ();
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Count);
        }
    }
}