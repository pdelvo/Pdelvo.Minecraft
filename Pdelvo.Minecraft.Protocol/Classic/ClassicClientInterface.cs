using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Classic.Packets;

namespace Pdelvo.Minecraft.Protocol.Classic
{
    public class ClassicClientInterface : RemoteInterface
    {
        public ClassicClientInterface(BigEndianStream stream, int version)
            : base(new PacketEndPoint(stream, version))
        {
            //_thread.Name = "Client Thread";
            stream.Context = PacketContext.Client;

            PrepareEndPoint ();
        }

        /// <summary>
        ///   Prepares the end point.
        /// </summary>
        /// <remarks>
        /// </remarks>
        protected void PrepareEndPoint()
        {
            RegisterPacket<PlayerIdentification>(0x00);
            RegisterPacket<SetBlock>(0x05);
            RegisterPacket<PositionAndOrientation>(0x08);
            RegisterPacket<Message>(0x0D);
        }
    }
}