using System;
using System.Globalization;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ClientToServer)]
    public class HandshakeRequest : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandshakeRequest"/> class.
        /// </summary>
        /// <remarks></remarks>
        public HandshakeRequest()
        {
            Code = 2;
        }

        public byte ProtocolVersion { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        /// <remarks></remarks>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        /// <remarks></remark>
        public string Host { get; set; }

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
            int i = reader.Peek();//if i == 0 it is the first byte of the Username string(because the length is normally < 255). if it is not 0 it is the protocol version
            if (i != 0)
                ProtocolVersion = reader.ReadByte();
            UserName = reader.ReadString16();
            if (UserName.Contains(";"))
            {
                Host = UserName.Substring(UserName.IndexOf(";", StringComparison.Ordinal) + 1);
                UserName = UserName.Substring(0, UserName.IndexOf(";", StringComparison.Ordinal));
            }
            if (ProtocolVersion >= 32)
            {
                string host = reader.ReadString16();
                int port = reader.ReadInt32();
                Host = host + ":" + port;
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
            if(version >=31)
            writer.Write(ProtocolVersion);
            if (version > 23)
                writer.Write(UserName + (string.IsNullOrEmpty(Host) || version >= 31 ? "" : ";" + Host));
            else
                writer.Write(UserName);

            if (version >= 32)
            {
                if (Host == null) Host = "localhost:25565";
                string[] splitted = Host.Split(':');
                string host = splitted[0];
                int port = splitted.Length > 1 ? int.Parse(splitted[1], CultureInfo.InvariantCulture) : 25565;
                writer.Write(host);
                writer.Write(port);
            }
        }
    }
}