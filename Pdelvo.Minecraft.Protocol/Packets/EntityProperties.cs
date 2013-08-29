using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class EntityProperties : Packet, IEntityPacket
    {
        #region IEntityPacket Members

        /// <summary>
        ///   Gets or sets the entity Id.
        /// </summary>
        /// <value> The entity Id. </value>
        /// <remarks>
        /// </remarks>
        public int EntityId
        {
            get;
            set;
        }

        #endregion 

        public List<PropertyData> Properties { get; set; } 

        /// <summary>
        ///   Initializes a new instance of the <see cref="EmptyPacket" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public EntityProperties()
        {
            Code = 0x2C;
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

            EntityId = reader.ReadInt32();

            var count = reader.ReadInt32();

            Properties = new List<PropertyData>();
            for (int i = 0; i < count; i++)
            {
                Properties.Add(PropertyData.Read(reader));
            }
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

            writer.Write(EntityId);

            Properties = Properties ?? new List<PropertyData>();

            writer.Write(Properties.Count);

            foreach (var property in Properties)
            {
                property.Write(writer);
            }
        }

        public class PropertyData
        {
            public string Key { get; set; }
            public double Value { get; set; }
            public List<ModifierData> InnerData { get; set; } 

            public static PropertyData Read(BigEndianStream stream)
            {
                var data = new PropertyData
                    {
                        Key = stream.ReadString16(),
                        Value = stream.ReadDouble(),
                        InnerData = new List<ModifierData>()
                    };

                var length = stream.ReadInt16();
                for (int i = 0; i < length; i++)
                {
                    data.InnerData.Add(ModifierData.Read(stream));
                }
                return data;
            }

            public void Write(BigEndianStream stream)
            {
                stream.Write(Key);
                stream.Write(Value);
                stream.Write((short)InnerData.Count);

                foreach (var modifierData in InnerData)
                {
                    modifierData.Write(stream);
                }
            }
        }

        public class ModifierData
        {
            public Uuid Id { get; set; }
            public double Amount { get; set; }
            public byte Operation { get; set; }

            public static ModifierData Read(BigEndianStream stream)
            {
                return new ModifierData
                    {
                        Id = new Uuid(stream),
                        Amount = stream.ReadDouble(),
                        Operation = stream.ReadByte()
                    };
            }

            public void Write(BigEndianStream stream)
            {
                Id.Write(stream);
                stream.Write(Amount);
                stream.Write(Operation);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Uuid
        {
            public Uuid(BigEndianStream stream)
            {
                High = stream.ReadInt64();
                Low = stream.ReadInt64();
            }
            public long High;
            public long Low;

            public void Write(BigEndianStream stream)
            {
                stream.Write(High);
                stream.Write(Low);
            }
        }
    }
}
