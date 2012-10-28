using System.Collections.Generic;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [LastSupportedVersion(27, true)]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class MultiBlockChangeOld : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiBlockChangeOld"/> class.
        /// </summary>
        /// <remarks></remarks>
        public MultiBlockChangeOld()
        {
            Code = 0x34;
            CoordiateArray = new short[0];
            TypeArray = new byte[0];
            MetadataArray = new byte[0];
        }

        /// <summary>
        /// Gets or sets the chunk X.
        /// </summary>
        /// <value>The chunk X.</value>
        /// <remarks></remarks>
        public int ChunkX { get; set; }
        /// <summary>
        /// Gets or sets the chunk Z.
        /// </summary>
        /// <value>The chunk Z.</value>
        /// <remarks></remarks>
        public int ChunkZ { get; set; }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        /// <remarks></remarks>
        public short Size { get; set; }
        /// <summary>
        /// Gets or sets the coordiate array.
        /// </summary>
        /// <value>The coordiate array.</value>
        /// <remarks></remarks>
        public IEnumerable<short> CoordiateArray { get; set; }
        /// <summary>
        /// Gets or sets the type array.
        /// </summary>
        /// <value>The type array.</value>
        /// <remarks></remarks>
        public IEnumerable<byte> TypeArray { get; set; }
        /// <summary>
        /// Gets or sets the metadata array.
        /// </summary>
        /// <value>The metadata array.</value>
        /// <remarks></remarks>
        public IEnumerable<byte> MetadataArray { get; set; }

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
            ChunkX = reader.ReadInt32();
            ChunkZ = reader.ReadInt32();
            Size = reader.ReadInt16();
            var coordinates = new List<short>();
            var types = new List<byte>();
            var metadata = new List<byte>();

            for (int i = 0; i < Size; i++)
            {
                coordinates.Add(reader.ReadInt16());
            }

            for (int i = 0; i < Size; i++)
            {
                types.Add(reader.ReadByte());
            }

            for (int i = 0; i < Size; i++)
            {
                metadata.Add(reader.ReadByte());
            }

            CoordiateArray = coordinates;
            TypeArray = types;
            MetadataArray = metadata;
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
            writer.Write(ChunkX);
            writer.Write(ChunkZ);
            writer.Write(Size);

            foreach (short item in CoordiateArray)
            {
                writer.Write(item);
            }

            foreach (byte item in TypeArray)
            {
                writer.Write(item);
            }

            foreach (byte item in MetadataArray)
            {
                writer.Write(item);
            }
        }
    }
}