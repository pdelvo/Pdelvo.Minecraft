using System;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class PacketEventArgs : EventArgs
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="PacketEventArgs" /> class.
        /// </summary>
        /// <param name="packet"> The packet. </param>
        /// <remarks>
        /// </remarks>
        public PacketEventArgs(Packet packet)
        {
            Packet = packet;
        }

        /// <summary>
        ///   Gets or sets the packet.
        /// </summary>
        /// <value> The packet. </value>
        /// <remarks>
        /// </remarks>
        public Packet Packet { get; set; }
    }
}