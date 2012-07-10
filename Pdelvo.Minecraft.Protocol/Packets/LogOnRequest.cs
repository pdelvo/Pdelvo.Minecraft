using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class LogOnRequest : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogOnRequest"/> class.
        /// </summary>
        /// <remarks></remarks>
        public LogOnRequest()
        {
            Code = 1;
            Unknown = "";
        }

        /// <summary>
        /// Gets or sets the protocol version.
        /// </summary>
        /// <value>The protocol version.</value>
        /// <remarks></remarks>
        [Obsolete("In newer version of the protocol the protocol version is not longer sent by the LogOnRequest packet")]
        public int ProtocolVersion { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        /// <remarks></remarks>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the unknown.
        /// </summary>
        /// <value>The unknown.</value>
        /// <remarks></remarks>
        public string Unknown { get; set; }

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
            if (version >= 32)
                return;
            ProtocolVersion = reader.ReadInt32();
            UserName = reader.ReadString16();
            if (ProtocolVersion != 0)
                version = ProtocolVersion;
            //Not used
            if (version <= 27)
                reader.ReadInt64();
            if (version >= 23 && version <= 30)
                Unknown = reader.ReadString16();
            reader.ReadInt32();
            if (version >= 27)
                reader.ReadInt32();
            else
                reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
            reader.ReadByte();
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
            if (version >= 32)
                return;
            if (ProtocolVersion != 0)
                version = ProtocolVersion;
            writer.Write(version);
            writer.Write(UserName);
            //Not used
            if (version <= 27)
                writer.Write((long) 0);
            if (version >= 23 && version <= 30)
                writer.Write(Unknown);

            writer.Write(0);
            if (version >= 27)
                writer.Write(0);
            else
                writer.Write((byte) 0);
            writer.Write((Byte) 0);
            writer.Write((byte) 0);
            writer.Write((byte) 0);
        }
    }
}