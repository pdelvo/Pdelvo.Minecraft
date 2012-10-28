using System;
using System.Collections.Generic;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.Both)]
    public class Explosion : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Explosion"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Explosion()
        {
            Code = 0x3C;

            Records = new ExplosionRecord[0];
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        /// <remarks></remarks>
        public double PositionX { get; set; }
        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        /// <remarks></remarks>
        public double PositionY { get; set; }
        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        /// <remarks></remarks>
        public double PositionZ { get; set; }
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        /// <remarks></remarks>
        public float Radius { get; set; }
        /// <summary>
        /// Gets or sets the record count.
        /// </summary>
        /// <value>The record count.</value>
        /// <remarks></remarks>
        public int RecordCount { get; set; }
        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>The records.</value>
        /// <remarks></remarks>
        public IEnumerable<ExplosionRecord> Records { get; set; }

        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public float VelocityZ { get; set; }

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
            PositionX = reader.ReadDouble();
            PositionY = reader.ReadDouble();
            PositionZ = reader.ReadDouble();
            Radius = reader.ReadSingle();
            var records = new ExplosionRecord[RecordCount = reader.ReadInt32()];
            for (int i = 0; i < RecordCount; i++)
            {
                var record = new ExplosionRecord();
                record.OffsetX = reader.ReadByte();
                record.OffsetY = reader.ReadByte();
                record.OffsetZ = reader.ReadByte();
                records[i] = record;
            }
            Records = records;
            if (version >= 36)
            {
                VelocityX = reader.ReadSingle();
                VelocityY = reader.ReadSingle();
                VelocityZ = reader.ReadSingle();
            }
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Radius);
            writer.Write(RecordCount);
            foreach (ExplosionRecord item in Records)
            {
                writer.Write(item.OffsetX);
                writer.Write(item.OffsetY);
                writer.Write(item.OffsetZ);
            }

            if (version >= 36)
            {
                writer.Write(VelocityX);
                writer.Write(VelocityY);
                writer.Write(VelocityZ);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class ExplosionRecord
    {
        /// <summary>
        /// Gets or sets the offset X.
        /// </summary>
        /// <value>The offset X.</value>
        /// <remarks></remarks>
        public byte OffsetX { get; set; }
        /// <summary>
        /// Gets or sets the offset Y.
        /// </summary>
        /// <value>The offset Y.</value>
        /// <remarks></remarks>
        public byte OffsetY { get; set; }
        /// <summary>
        /// Gets or sets the offset Z.
        /// </summary>
        /// <value>The offset Z.</value>
        /// <remarks></remarks>
        public byte OffsetZ { get; set; }
    }
}