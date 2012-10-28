using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class StanceUpdate : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StanceUpdate"/> class.
        /// </summary>
        /// <remarks></remarks>
        public StanceUpdate()
        {
            Code = 0x1B;
        }

        /// <summary>
        /// Gets or sets the unknown1.
        /// </summary>
        /// <value>The unknown1.</value>
        /// <remarks></remarks>
        public float Unknown1 { get; set; }
        /// <summary>
        /// Gets or sets the unknown2.
        /// </summary>
        /// <value>The unknown2.</value>
        /// <remarks></remarks>
        public float Unknown2 { get; set; }
        /// <summary>
        /// Gets or sets the unknown3.
        /// </summary>
        /// <value>The unknown3.</value>
        /// <remarks></remarks>
        public float Unknown3 { get; set; }
        /// <summary>
        /// Gets or sets the unknown4.
        /// </summary>
        /// <value>The unknown4.</value>
        /// <remarks></remarks>
        public float Unknown4 { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StanceUpdate"/> is unknown5.
        /// </summary>
        /// <value><c>true</c> if unknown5; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool Unknown5 { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StanceUpdate"/> is unknown6.
        /// </summary>
        /// <value><c>true</c> if unknown6; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool Unknown6 { get; set; }

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
            Unknown1 = reader.ReadSingle();
            Unknown2 = reader.ReadSingle();
            Unknown3 = reader.ReadSingle();
            Unknown4 = reader.ReadSingle();
            Unknown5 = reader.ReadBoolean();
            Unknown6 = reader.ReadBoolean();
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
            writer.Write(Unknown1);
            writer.Write(Unknown2);
            writer.Write(Unknown3);
            writer.Write(Unknown4);
            writer.Write(Unknown5);
            writer.Write(Unknown6);
        }
    }
}