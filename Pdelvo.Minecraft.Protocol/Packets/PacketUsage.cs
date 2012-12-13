using System;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PacketUsageAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        private readonly PacketUsage _packetUsage;

        // This is a positional argument
        public PacketUsageAttribute(PacketUsage packetUsage)
        {
            _packetUsage = packetUsage;
        }

        public PacketUsage PacketUsage
        {
            get { return _packetUsage; }
        }
    }

    public enum PacketUsage
    {
        None = 0x00,
        ServerToClient = 0x01,
        ClientToServer = 0x02,
        Both = ServerToClient | ClientToServer
    }
}