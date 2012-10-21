using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class SoundEffect : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundEffect"/> class.
        /// </summary>
        /// <remarks></remarks>
        public SoundEffect()
        {
            Code = 0x3D;
        }

        /// <summary>
        /// Gets or sets the effect Id.
        /// </summary>
        /// <value>The effect Id.</value>
        /// <remarks></remarks>
        public int EffectId { get; set; }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        /// <remarks></remarks>
        public int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        /// <remarks></remarks>
        public byte PositionY { get; set; }

        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        /// <remarks></remarks>
        public int PositionZ { get; set; }

        /// <summary>
        /// Gets or sets the sound data.
        /// </summary>
        /// <value>The sound data.</value>
        /// <remarks></remarks>
        public int SoundData { get; set; }

        /// <summary>
        /// True if the volume will not decrease if the distance between player and source is big
        /// </summary>
        /// <value>NoVolumeDecrease</value>
        /// <remarks></remarks>
        public bool NoVolumeDecrease { get; set; }

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
            EffectId = reader.ReadInt32();
            PositionX = reader.ReadInt32();
            PositionY = reader.ReadByte();
            PositionZ = reader.ReadInt32();
            SoundData = reader.ReadInt32();

            if (version >= 47)
            {
                NoVolumeDecrease = reader.ReadBoolean();
            }
            else
            {
                NoVolumeDecrease = EffectId == 1013;
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
                throw new System.ArgumentNullException("writer");
            writer.Write(Code);
            writer.Write(EffectId);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(SoundData);
            if (version >= 47)
            {
                writer.Write(NoVolumeDecrease);
            }
        }
    }
}