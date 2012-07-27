using Pdelvo.Minecraft.Protocol.Helper;
using Pdelvo.Minecraft.Protocol.Packets;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// A remote interface to handle a connection to a default minecraft server
    /// </summary>
    /// <remarks></remarks>
    public class ServerRemoteInterface : RemoteInterface
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerRemoteInterface"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        public ServerRemoteInterface(BigEndianStream stream, int version)
            : base(new PacketEndPoint(stream, version))
        {
            //_thread.Name = "Server Thread";
            stream.Context = PacketContext.Server;

            PrepareEndPoint();
        }

        /// <summary>
        /// Prepares the end point.
        /// </summary>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification="This is neccecery")]
        protected void PrepareEndPoint()
        {
            //Server only
            RegisterPacket<LogOnResponse>(0x01);
            RegisterPacket<HandshakeResponse>(0x02);
            RegisterPacket<TimeUpdatePacket>(0x04);
            RegisterPacket<SpawnPosition>(0x06);
            RegisterPacket<UpdateHealth>(0x08);
            RegisterPacket<PlayerPositionLookResponse>(0x0D);
            RegisterPacket<UseBed>(0x11);
            RegisterPacket<NamedEntitySpawn>(0x14);
            RegisterPacket<CollectItem>(0x16);
            RegisterPacket<AddObject>(0x17);
            RegisterPacket<MobSpawn>(0x18);
            RegisterPacket<ExperienceOrb>(0x1A);
            RegisterPacket<EntityDestroy>(0x1D);
            RegisterPacket<Entity>(0x1E);
            RegisterPacket<EntityRelativeMove>(0x1F);
            RegisterPacket<EntityLook>(0x20);
            RegisterPacket<EntityLookAndRelativeMove>(0x21);
            RegisterPacket<EntityTeleport>(0x22);
            RegisterPacket<EntityStatus>(0x26);
            RegisterPacket<PreChunk>(0x32);
            RegisterPacket<MapChunk>(0x33);
            RegisterPacket<MapChunkOld>(0x33);
            RegisterPacket<MultiBlockChange>(0x34);
            RegisterPacket<MultiBlockChangeOld>(0x34);
            RegisterPacket<BlockAction>(0x36);
            RegisterPacket<DiggingProgressPacket>(0x37);
            RegisterPacket<MapChunkBulkPacket>(0x38);
            RegisterPacket<NamedSoundEffect>(0x3E);
            RegisterPacket<Thunderbolt>(0x47);
            RegisterPacket<SetSlot>(0x67);
            RegisterPacket<WindowItems>(0x68);
            RegisterPacket<UpdateProgressBar>(0x69);
            RegisterPacket<ItemData>(0x83);
            RegisterPacket<UpdateTileEntity>(0x84);
            RegisterPacket<EncryptionKeyResponse>(0xFC);
            RegisterPacket<EncryptionKeyRequest>(0xFD);

            //General packets
            ProtocolHelper.RegisterGeneralPackets(EndPoint);
        }
    }
}