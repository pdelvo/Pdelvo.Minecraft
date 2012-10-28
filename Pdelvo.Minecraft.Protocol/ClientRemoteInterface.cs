using Pdelvo.Minecraft.Protocol.Helper;
using Pdelvo.Minecraft.Protocol.Packets;
using Pdelvo.Minecraft.Network;
using System.IO;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// A remote interface to handle a connection to a default minecraft client
    /// </summary>
    /// <remarks></remarks>
    public class ClientRemoteInterface : RemoteInterface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRemoteInterface"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        public ClientRemoteInterface(BigEndianStream stream, int version)
            : base(new PacketEndPoint(stream, version))
        {
            //_thread.Name = "Client Thread";
            stream.Context = PacketContext.Client;

            PrepareEndPoint();
        }

        /// <summary>
        /// Prepares the end point.
        /// </summary>
        /// <remarks></remarks>
        protected void PrepareEndPoint()
        {
            ProtocolHelper.RegisterPackets(EndPoint, PacketUsage.ClientToServer);
        }

        public static ClientRemoteInterface Create(Stream baseStream, int protocolVersion)
        {
            return new ClientRemoteInterface(new BigEndianStream(
                new FullyReadStream(baseStream)), protocolVersion);
        }
    }
}