using System.Diagnostics.CodeAnalysis;
using System.IO;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Helper;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    ///   A remote interface to handle a connection to a default minecraft server
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class ServerRemoteInterface : RemoteInterface
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ServerRemoteInterface" /> class.
        /// </summary>
        /// <param name="stream"> The stream. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        public ServerRemoteInterface(BigEndianStream stream, int version)
            : base(new PacketEndPoint(stream, version))
        {
            //_thread.Name = "Server Thread";
            stream.Context = PacketContext.Server;

            PrepareEndPoint ();
        }

        /// <summary>
        ///   Prepares the end point.
        /// </summary>
        /// <remarks>
        /// </remarks>
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling",
            Justification = "This is neccecery")]
        protected void PrepareEndPoint()
        {
            ProtocolHelper.RegisterPackets(EndPoint, PacketUsage.ServerToClient);
        }

        public static ServerRemoteInterface Create(Stream baseStream, int protocolVersion)
        {
            return new ServerRemoteInterface(new BigEndianStream(
                                                 new FullyReadStream(baseStream)), protocolVersion);
        }
    }
}