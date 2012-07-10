using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class UseEntity : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UseEntity"/> class.
        /// </summary>
        /// <remarks></remarks>
        public UseEntity()
        {
            Code = 0x07;
        }

        /// <summary>
        /// Gets or sets the user entity.
        /// </summary>
        /// <value>The user entity.</value>
        /// <remarks></remarks>
        public int UserEntity { get; set; }
        /// <summary>
        /// Gets or sets the target entity.
        /// </summary>
        /// <value>The target entity.</value>
        /// <remarks></remarks>
        public int TargetEntity { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [left click].
        /// </summary>
        /// <value><c>true</c> if [left click]; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool LeftClick { get; set; }

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
            UserEntity = reader.ReadInt32();
            TargetEntity = reader.ReadInt32();
            LeftClick = reader.ReadBoolean();
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
            writer.Write(UserEntity);
            writer.Write(TargetEntity);
            writer.Write(LeftClick);
        }
    }
}