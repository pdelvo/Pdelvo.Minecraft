using System.Collections.Generic;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class MetadataInfo
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        /// <remarks></remarks>
        public Dictionary<byte, object> Data { get; private set; }

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        /// <remarks></remarks>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "version", Justification="For future compatibility")]
        public static MetadataInfo Read(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new System.ArgumentNullException("reader");
            byte b = 0;
            var data = new Dictionary<byte, object>();
            while ((b = reader.ReadByte()) != 127)
            {
                switch (b >> 5)
                {
                    case 0:
                        data.Add(b, reader.ReadByte());
                        break;
                    case 1:
                        data.Add(b, reader.ReadInt16());
                        break;
                    case 2:
                        data.Add(b, reader.ReadInt32());
                        break;
                    case 3:
                        data.Add(b, reader.ReadSingle());
                        break;
                    case 4:
                        data.Add(b, reader.ReadString16());
                        break;
                    case 5:
                        data.Add(b, ItemStack.Read(reader));
                        break;
                    case 6:
                        data.Add(b,
                                 new EntityInformation
                                     {Data1 = reader.ReadInt32(), Data2 = reader.ReadInt32(), Data3 = reader.ReadInt32()});
                        break;
                    default:
                        break;
                }
            }
            return new MetadataInfo {Data = data};
        }

        /// <summary>
        /// Writes the metadata.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="writer">The writer.</param>
        /// <remarks></remarks>
        public static void WriteMetadata(MetadataInfo data, BigEndianStream writer)
        {
            if (writer == null)
                throw new System.ArgumentNullException("writer");
            if (data == null)
            {
                writer.Write((byte) 127);
                return;
            }

            foreach (var item in data.Data)
            {
                if (item.Value is byte)
                {
                    writer.Write(item.Key);
                    writer.Write((byte) item.Value);
                }
                else if (item.Value is short)
                {
                    writer.Write(item.Key);
                    writer.Write((short) item.Value);
                }
                else if (item.Value is int)
                {
                    writer.Write(item.Key);
                    writer.Write((int) item.Value);
                }
                else if (item.Value is float)
                {
                    writer.Write(item.Key);
                    writer.Write((float) item.Value);
                }
                else if (item.Value is string)
                {
                    writer.Write(item.Key);
                    writer.Write((string) item.Value);
                }
                else if (item.Value is ItemStack)
                {
                    writer.Write(item.Key);
                    writer.Write((ItemStack) item.Value);
                }
                else if (item.Value is EntityInformation)
                {
                    writer.Write(item.Key);
                    writer.Write((EntityInformation) item.Value);
                }
            }
            writer.Write((byte) 127);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class EntityInformation
    {
        /// <summary>
        /// Gets or sets the data1.
        /// </summary>
        /// <value>The data1.</value>
        /// <remarks></remarks>
        public int Data1 { get; set; }
        /// <summary>
        /// Gets or sets the data2.
        /// </summary>
        /// <value>The data2.</value>
        /// <remarks></remarks>
        public int Data2 { get; set; }
        /// <summary>
        /// Gets or sets the data3.
        /// </summary>
        /// <value>The data3.</value>
        /// <remarks></remarks>
        public int Data3 { get; set; }
    }
}