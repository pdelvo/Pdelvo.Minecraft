using System.Collections.Generic;
using Pdelvo.Minecraft.Network;
using System.Linq;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [LastSupportedVersion(26, throwException: true)]
    public class MapChunkOld : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapChunkOld"/> class.
        /// </summary>
        /// <remarks></remarks>
        public MapChunkOld()
        {
            Code = 0x33;
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
        /// Gets or sets the size X.
        /// </summary>
        /// <value>The size X.</value>
        /// <remarks></remarks>
        public byte SizeX { get; set; }
        /// <summary>
        /// Gets or sets the size Y.
        /// </summary>
        /// <value>The size Y.</value>
        /// <remarks></remarks>
        public byte SizeY { get; set; }
        /// <summary>
        /// Gets or sets the size Z.
        /// </summary>
        /// <value>The size Z.</value>
        /// <remarks></remarks>
        public byte SizeZ { get; set; }

        /// <summary>
        /// Gets or sets the size of the compressed.
        /// </summary>
        /// <value>The size of the compressed.</value>
        /// <remarks></remarks>
        public int CompressedSize { get; set; }
        /// <summary>
        /// Gets or sets the compressed data.
        /// </summary>
        /// <value>The compressed data.</value>
        /// <remarks></remarks>
        public IEnumerable<byte> CompressedData { get; set; }

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

            SizeX = reader.ReadByte();
            SizeY = reader.ReadByte();
            SizeZ = reader.ReadByte();

            CompressedSize = reader.ReadInt32();
            CompressedData = reader.ReadBytes(CompressedSize);
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

            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);

            writer.Write(SizeX);
            writer.Write(SizeY);
            writer.Write(SizeZ);

            writer.Write(CompressedSize);
            writer.Write(CompressedData.ToArray());
        }
    }
}