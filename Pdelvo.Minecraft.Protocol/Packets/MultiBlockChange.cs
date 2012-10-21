using System.Collections.Generic;
using Pdelvo.Minecraft.Network;
using System.Linq;


namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [RequireVersion(versionNumber: 28, throwException: true)]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class MultiBlockChange : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiBlockChange"/> class.
        /// </summary>
        /// <remarks></remarks>
        public MultiBlockChange()
        {
            Code = 0x34;
            DataArray = new byte[0];
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
        /// Gets or sets the unknown.
        /// </summary>
        /// <value>The unknown.</value>
        /// <remarks></remarks>
        public short Unknown { get; set; }
        /// <summary>
        /// Gets or sets the data array.
        /// </summary>
        /// <value>The data array.</value>
        /// <remarks></remarks>
        public IEnumerable<byte> DataArray { get; set; }

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
            Unknown = reader.ReadInt16();
            DataArray = reader.ReadBytes(reader.ReadInt32());
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
            writer.Write(Unknown);
            writer.Write(DataArray.Count());
            writer.Write(DataArray.ToArray());
        }
    }
}