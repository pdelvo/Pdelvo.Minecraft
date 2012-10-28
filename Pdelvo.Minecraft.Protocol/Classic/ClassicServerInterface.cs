using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Classic.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic
{
    public class ClassicServerInterface : RemoteInterface
    {
        public ClassicServerInterface(BigEndianStream stream, int version)
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
            RegisterPacket<ServerIdentification>(0x00);
            RegisterPacket<Ping>(0x01);
            RegisterPacket<LevelInitialize>(0x02);
            RegisterPacket<LevelDataChunk>(0x03);
            RegisterPacket<LevelFinalize>(0x04);
            RegisterPacket<SetServerBlock>(0x06);
            RegisterPacket<SpawnPlayer>(0x07);
            RegisterPacket<PositionAndOrientation>(0x08);
            RegisterPacket<PositionAndOrientationUpdate>(0x09);
            RegisterPacket<PositionUpdate>(0x0A);
            RegisterPacket<OrientationUpdate>(0x0B);
            RegisterPacket<DespawnPlayer>(0x0C);
            RegisterPacket<Message>(0x0D);
            RegisterPacket<DisconnectPlayer>(0x0E);
            RegisterPacket<UpdateUserType>(0x0F);
        }
    }
}
