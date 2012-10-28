using System;
using System.Threading.Tasks;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public interface IMinecraftRemoteInterface
    {
        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <remarks></remarks>
        PacketEndPoint EndPoint { get; }
        /// <summary>
        /// Occurs when [packet received].
        /// </summary>
        /// <remarks></remarks>
        event EventHandler<PacketEventArgs> PacketReceived;
        /// <summary>
        /// Occurs when [aborted].
        /// </summary>
        /// <remarks></remarks>
        event EventHandler<RemoteInterfaceAbortedEventArgs> Aborted;
        /// <summary>
        /// Sends the packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <remarks></remarks>
        void SendPacketQueued(Packet packet);
        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <remarks></remarks>
        Task Run();
        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        /// <remarks></remarks>
        void Shutdown();
        /// <summary>
        /// Registers the packet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification="The generic is used to have compile time error checking.")]
        void RegisterPacket<T>(byte id) where T : Packet, new();
    }
}