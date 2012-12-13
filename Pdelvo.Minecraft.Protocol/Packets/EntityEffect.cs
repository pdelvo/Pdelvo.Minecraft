using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.Both)]
    public class EntityEffect : Packet, IEntityPacket
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="EntityEffect" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public EntityEffect()
        {
            Code = 0x29;
        }

        /// <summary>
        ///   Gets or sets the effect Id.
        /// </summary>
        /// <value> The effect Id. </value>
        /// <remarks>
        /// </remarks>
        public byte EffectId { get; set; }

        /// <summary>
        ///   Gets or sets the amplifier.
        /// </summary>
        /// <value> The amplifier. </value>
        /// <remarks>
        /// </remarks>
        public byte Amplifier { get; set; }

        /// <summary>
        ///   Gets or sets the duration.
        /// </summary>
        /// <value> The duration. </value>
        /// <remarks>
        /// </remarks>
        public short Duration { get; set; }

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
            EffectId = reader.ReadByte ();
            Amplifier = reader.ReadByte ();
            Duration = reader.ReadInt16 ();
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
            writer.Write(EffectId);
            writer.Write(Amplifier);
            writer.Write(Duration);
        }
    }
}