using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PacketUsageAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly PacketUsage _packetUsage;

        // This is a positional argument
        public PacketUsageAttribute(PacketUsage packetUsage)
        {
            this._packetUsage = packetUsage;
        }

        public PacketUsage PacketUsage
        {
            get { return _packetUsage; }
        }
    }

    public enum PacketUsage : int
    {
        None = 0x00,
        ServerToClient = 0x01,
        ClientToServer = 0x02,
        Both = ServerToClient | ClientToServer
    }
}
