using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class Experience : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Experience"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Experience()
        {
            Code = 0x2B;
        }

        /// <summary>
        /// Gets or sets the current experience.
        /// </summary>
        /// <value>The current experience.</value>
        /// <remarks></remarks>
        public float CurrentExperience { get; set; } // Changed to float in 20
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        /// <remarks></remarks>
        public short Level { get; set; } //Changed to short in 20
        /// <summary>
        /// Gets or sets the total experience.
        /// </summary>
        /// <value>The total experience.</value>
        /// <remarks></remarks>
        public short TotalExperience { get; set; }

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
            CurrentExperience = reader.ReadSingle();
            Level = reader.ReadInt16();
            TotalExperience = reader.ReadInt16();
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
            writer.Write(CurrentExperience);
            writer.Write(Level);
            writer.Write(TotalExperience);
        }
    }
}