using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol.Packets;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Pdelvo.Minecraft.Protocol
{
    public static class LoginExtensions
    {
        public static async Task<AdditionalServerListInformation> ReadAdditionalServerListInformationAsync(this ClientRemoteInterface clientInterface)
        {
            var packet = await clientInterface.ReadPacketAsync() as PluginMessage;

            if (packet == null || packet.Channel != "MC|PingHost")
                return null;

            var innerStream = new BigEndianStream(new MemoryStream(packet.InternalData.ToArray()));

            return new AdditionalServerListInformation
            {
                ProtocolVersion = innerStream.ReadByte(),
                Host = innerStream.ReadString16(),
                Port = innerStream.ReadInt32()
            };
        }
    }

    public class AdditionalServerListInformation
    {
        public byte ProtocolVersion { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
