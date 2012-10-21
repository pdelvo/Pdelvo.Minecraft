using System;
using System.Collections.Generic;
using System.Linq;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class EntityDestroy : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDestroy"/> class.
        /// </summary>
        /// <remarks></remarks>
        public EntityDestroy()
        {
            Code = 0x1D;
            EntityIds = Enumerable.Empty<int>();
        }

        /// <summary>
        /// Gets or sets the entity Id.
        /// </summary>
        /// <value>The entity Id.</value>
        /// <remarks></remarks>
        public IEnumerable<int> EntityIds { get; set; }

        /// <summary>
        /// Receives the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            List<int> items = new List<int>();
            if (version >= 38)
            {
                int count = reader.ReadByte();
                for (int i = 0; i < count; i++)
                {
                    items.Add(reader.ReadInt32());
                }
            }
            else
            {
                items.Add(reader.ReadInt32());
            }
            EntityIds = items;
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
            if (version >= 38)
            {
                writer.Write(Code);
                writer.Write((byte)EntityIds.Count());
                foreach (var item in EntityIds)
                {
                    writer.Write(item);
                }
            }
            else
            {
                if (EntityIds == null || EntityIds.Count() == 0)
                {
                    writer.Write(Code);
                    writer.Write(0);
                }
                else
                foreach (var item in EntityIds)
                {
                    writer.Write(Code);
                    writer.Write(item);
                }
            }
        }
    }
}