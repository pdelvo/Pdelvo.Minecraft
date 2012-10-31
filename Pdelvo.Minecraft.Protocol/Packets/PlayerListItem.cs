using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.Both)]
    public class PlayerListItem : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="PlayerListItem" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public PlayerListItem()
        {
            Code = 0xC9;
        }

        /// <summary>
        ///   Gets or sets the name of the player.
        /// </summary>
        /// <value> The name of the player. </value>
        /// <remarks>
        /// </remarks>
        public string PlayerName { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="PlayerListItem" /> is online.
        /// </summary>
        /// <value> <c>true</c> if online; otherwise, <c>false</c> . </value>
        /// <remarks>
        /// </remarks>
        public bool Online { get; set; }

        /// <summary>
        ///   Gets or sets the ping.
        /// </summary>
        /// <value> The ping. </value>
        /// <remarks>
        /// </remarks>
        public short Ping { get; set; }

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
            PlayerName = reader.ReadString16 ();
            Online = reader.ReadBoolean ();
            Ping = reader.ReadInt16 ();
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
            writer.Write(PlayerName);
            writer.Write(Online);
            writer.Write(Ping);
        }
    }
}