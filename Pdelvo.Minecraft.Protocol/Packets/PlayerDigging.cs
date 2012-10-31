using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.Both)]
    public class PlayerDigging : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="PlayerDigging" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public PlayerDigging()
        {
            Code = 0x0E;
        }

        /// <summary>
        ///   Gets or sets the status.
        /// </summary>
        /// <value> The status. </value>
        /// <remarks>
        /// </remarks>
        public byte Status { get; set; }

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
        public byte PositionY { get; set; }

        /// <summary>
        ///   Gets or sets the Z.
        /// </summary>
        /// <value> The Z. </value>
        /// <remarks>
        /// </remarks>
        public int PositionZ { get; set; }

        /// <summary>
        ///   Gets or sets the face.
        /// </summary>
        /// <value> The face. </value>
        /// <remarks>
        /// </remarks>
        public byte Face { get; set; }

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
            Status = reader.ReadByte ();
            PositionX = reader.ReadInt32 ();
            PositionY = reader.ReadByte ();
            PositionZ = reader.ReadInt32 ();
            Face = reader.ReadByte ();
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
            writer.Write(Status);
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Face);
        }
    }
}