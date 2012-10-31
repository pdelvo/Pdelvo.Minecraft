using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class BlockAction : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="BlockAction" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public BlockAction()
        {
            Code = 0x36;
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
        public short PositionY { get; set; }

        /// <summary>
        ///   Gets or sets the Z.
        /// </summary>
        /// <value> The Z. </value>
        /// <remarks>
        /// </remarks>
        public int PositionZ { get; set; }

        /// <summary>
        ///   Gets or sets the byte1.
        /// </summary>
        /// <value> The byte1. </value>
        /// <remarks>
        /// </remarks>
        public byte Byte1 { get; set; }

        /// <summary>
        ///   Gets or sets the byte2.
        /// </summary>
        /// <value> The byte2. </value>
        /// <remarks>
        /// </remarks>
        public byte Byte2 { get; set; }

        public short UnknownInt16 { get; set; }

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
            PositionX = reader.ReadInt32 ();
            PositionY = reader.ReadInt16 ();
            PositionZ = reader.ReadInt32 ();
            Byte1 = reader.ReadByte ();
            Byte2 = reader.ReadByte ();

            if (version >= 38)
                UnknownInt16 = reader.ReadInt16 ();
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Byte1);
            writer.Write(Byte2);
            if (version >= 38)
                writer.Write(UnknownInt16);
        }
    }
}