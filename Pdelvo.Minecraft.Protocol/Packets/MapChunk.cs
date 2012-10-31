using System;
using System.Collections.Generic;
using System.Linq;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [RequireVersion(27, throwException: true)]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class MapChunk : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="MapChunk" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public MapChunk()
        {
            Code = 0x33;

            CompressedData = Enumerable.Empty<byte> ();
        }

        public override bool CanBeDelayed
        {
            get { return true; }
        }

        /// <summary>
        ///   Gets or sets the X.
        /// </summary>
        /// <value> The X. </value>
        /// <remarks>
        /// </remarks>
        public int PositionX { get; set; }

        /// <summary>
        ///   Gets or sets the Z.
        /// </summary>
        /// <value> The Z. </value>
        /// <remarks>
        /// </remarks>
        public int PositionZ { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether [ground up contiguous].
        /// </summary>
        /// <value> <c>true</c> if [ground up contiguous]; otherwise, <c>false</c> . </value>
        /// <remarks>
        /// </remarks>
        public bool GroundUpContiguous { get; set; }

        /// <summary>
        ///   Gets or sets the primary bitmap.
        /// </summary>
        /// <value> The primary bitmap. </value>
        /// <remarks>
        /// </remarks>
        public short PrimaryBitmap { get; set; }

        /// <summary>
        ///   Gets or sets the add bitmap.
        /// </summary>
        /// <value> The add bitmap. </value>
        /// <remarks>
        /// </remarks>
        public short AddBitmap { get; set; }

        /// <summary>
        ///   Gets or sets the size of the compressed.
        /// </summary>
        /// <value> The size of the compressed. </value>
        /// <remarks>
        /// </remarks>
        public int CompressedSize { get; set; }

        /// <summary>
        ///   Gets or sets the unused int.
        /// </summary>
        /// <value> The unused int. </value>
        /// <remarks>
        /// </remarks>
        public int UnusedInt32 { get; set; }

        /// <summary>
        ///   Gets or sets the compressed data.
        /// </summary>
        /// <value> The compressed data. </value>
        /// <remarks>
        /// </remarks>
        public IEnumerable<byte> CompressedData { get; set; }


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
            PositionZ = reader.ReadInt32 ();

            GroundUpContiguous = reader.ReadBoolean ();
            PrimaryBitmap = reader.ReadInt16 ();
            AddBitmap = reader.ReadInt16 ();

            CompressedSize = reader.ReadInt32 ();
            if (version < 38)
                UnusedInt32 = reader.ReadInt32 ();
            CompressedData = reader.ReadBytes(CompressedSize);
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
            writer.Write(PositionZ);

            writer.Write(GroundUpContiguous);
            writer.Write(PrimaryBitmap);
            writer.Write(AddBitmap);

            writer.Write(CompressedSize);
            if (version < 38)
                writer.Write(UnusedInt32);
            writer.Write(CompressedData.ToArray ());
        }
    }
}