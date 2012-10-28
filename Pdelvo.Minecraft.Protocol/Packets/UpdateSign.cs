using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class UpdateSign : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSign"/> class.
        /// </summary>
        /// <remarks></remarks>
        public UpdateSign()
        {
            Code = 0x82;
        }

        public override bool CanBeDelayed
        {
            get
            {
                return true;
            }
        }

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
        public short PositionY { get; set; }
        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        /// <remarks></remarks>
        public int PositionZ { get; set; }

        /// <summary>
        /// Gets or sets the line1.
        /// </summary>
        /// <value>The line1.</value>
        /// <remarks></remarks>
        public string Line1 { get; set; }
        /// <summary>
        /// Gets or sets the line2.
        /// </summary>
        /// <value>The line2.</value>
        /// <remarks></remarks>
        public string Line2 { get; set; }
        /// <summary>
        /// Gets or sets the line3.
        /// </summary>
        /// <value>The line3.</value>
        /// <remarks></remarks>
        public string Line3 { get; set; }
        /// <summary>
        /// Gets or sets the line4.
        /// </summary>
        /// <value>The line4.</value>
        /// <remarks></remarks>
        public string Line4 { get; set; }

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
            PositionX = reader.ReadInt32();
            PositionY = reader.ReadInt16();
            PositionZ = reader.ReadInt32();
            Line1 = reader.ReadString16();
            Line2 = reader.ReadString16();
            Line3 = reader.ReadString16();
            Line4 = reader.ReadString16();
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
            Line1 = Line1 ?? "";
            Line2 = Line2 ?? "";
            Line3 = Line3 ?? "";
            Line4 = Line4 ?? "";

            writer.Write(Code);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Line1.Length > 15 ? Line1.Substring(15) : Line1);
            writer.Write(Line2.Length > 15 ? Line2.Substring(15) : Line2);
            writer.Write(Line3.Length > 15 ? Line3.Substring(15) : Line3);
            writer.Write(Line4.Length > 15 ? Line4.Substring(15) : Line4);
        }
    }
}