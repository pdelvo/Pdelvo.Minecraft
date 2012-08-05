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
            //Client only
            RegisterPacket<LogOnRequest>(0x01);
            RegisterPacket<HandshakeRequest>(0x02);
            RegisterPacket<UseEntity>(0x07);
            RegisterPacket<Player>(0x0A);
            RegisterPacket<PlayerPosition>(0x0B);
            RegisterPacket<PlayerLook>(0x0C);
            RegisterPacket<PlayerPositionLookRequest>(0x0D);
            RegisterPacket<WindowClick>(0x66);
            RegisterPacket<PlayerListPing>(0xFE);
            RegisterPacket<SettingsChanged>(0xCC);
            RegisterPacket<RespawnRequestPacket>(0xCD);
            RegisterPacket<EncryptionKeyResponse>(0xFC);

            //General packets
            ProtocolHelper.RegisterGeneralPackets(EndPoint);
        }

        public static ClientRemoteInterface Create(Stream baseStream, int protocolVersion)
        {
            return new ClientRemoteInterface(new BigEndianStream(
                new FullyReadStream(baseStream)), protocolVersion);
        }
    }
}