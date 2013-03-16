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
    [PacketUsage(PacketUsage.ServerToClient)]
    public class MapChunkBulkPacket : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="MapChunkBulkPacket" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public MapChunkBulkPacket()
        {
            Code = 0x38;

            ChunkData = new byte[0];
            ChunkMetaData = Enumerable.Empty<ChunkBulkMetaData> ();
        }

        public short Count { get; set; }
        public int DataLength { get; set; } //length?
        public bool Unknown { get; set; }
        public byte[] ChunkData { get; set; }
        public IEnumerable<ChunkBulkMetaData> ChunkMetaData { get; set; }

        public override bool CanBeDelayed
        {
            get { return true; }
        }


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

            Count = reader.ReadInt16 ();
            DataLength = reader.ReadInt32 ();
            if (version >= 51)
                Unknown = reader.ReadBoolean ();
            ChunkData = reader.ReadBytes(DataLength);
            var list = new List<ChunkBulkMetaData> ();
            for (int i = 0; i < Count; i++)
            {
                list.Add(new ChunkBulkMetaData
                             {
                                 PositionX = reader.ReadInt32 (),
                                 PositionZ = reader.ReadInt32 (),
                                 PrimaryBitmap = reader.ReadInt16 (),
                                 AddBitmap = reader.ReadInt16 (),
                             });
            }
            ChunkMetaData = list;
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
            writer.Write(Count);
            writer.Write(DataLength);
            if (version >= 51)
                writer.Write(Unknown);
            writer.Write(ChunkData);

            foreach (ChunkBulkMetaData item in ChunkMetaData)
            {
                writer.Write(item.PositionX);
                writer.Write(item.PositionZ);
                writer.Write(item.PrimaryBitmap);
                writer.Write(item.AddBitmap);
            }
        }
    }

    public class ChunkBulkMetaData
    {
        public int PositionX { get; set; }
        public int PositionZ { get; set; }

        public short PrimaryBitmap { get; set; }
        public short AddBitmap { get; set; }
    }
}