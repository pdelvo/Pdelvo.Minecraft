using System;
using System.Collections.Generic;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
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

        public IDictionary<string, double> Properties { get; set; } 

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

            Properties = new Dictionary<string, double>();
            for (int i = 0; i < count; i++)
            {
                Properties.Add(reader.ReadString16(), reader.ReadDouble());
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

            Properties = Properties ?? new Dictionary<string, double>();

            writer.Write(Properties.Count);

            foreach (var property in Properties)
            {
                writer.Write(property.Key);
                writer.Write(property.Value);
            }
        }
    }
}
