using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pdelvo.Minecraft.Network;
using Pdelvo.Minecraft.Protocol;
using Pdelvo.Minecraft.Protocol.Helper;
using System.Linq;
using Pdelvo.Minecraft.Protocol.Packets;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Tests
{
    [TestClass]
    public class PacketTests
    {
        [TestMethod]
        public async Task ClientTest()
        {
            for (int i = ProtocolInformation.MinSupportedClientVersion;
                 i <= ProtocolInformation.MaxSupportedClientVersion;
                 i++)
            {
                var stream = new MemoryStream();
                var client = new ClientRemoteInterface(
                    new BigEndianStream(stream), i);
                foreach (Type item in client.EndPoint.GetPackets())
                {
                    stream = new MemoryStream();
                    client = new ClientRemoteInterface(
                        new BigEndianStream(stream), i);
                    if (PacketEndPoint.IsPacketSupported(item, i) == true)
                    {
                        try
                        {
                            var packet = Activator.CreateInstance(item) as Packet;
                            if (packet is HandshakeRequest) (packet as HandshakeRequest).ProtocolVersion = (byte)i;
                            await client.SendPacketAsync(packet);
                            stream.Seek(0, SeekOrigin.Begin);
                            client.EndPoint.Stream.Context = PacketContext.Server;
                            await client.ReadPacketAsync();
                            Assert.AreEqual(stream.Position, stream.Length, item.Name);
                        }
                        catch (Exception ex)
                        {
                            Assert.Fail(item.ToString(), ex);
                        }
                    }
                }
            }
        }
        [TestMethod]
        public async Task ServerTest()
        {
            for (int i = ProtocolInformation.MinSupportedServerVersion;
                 i <= ProtocolInformation.MaxSupportedServerVersion;
                 i++)
            {
                var stream = new MemoryStream();
                var client = new ServerRemoteInterface(
                    new BigEndianStream(stream), i);
                foreach (Type item in client.EndPoint.GetPackets())
                {
                    stream = new MemoryStream();
                    client = new ServerRemoteInterface(
                        new BigEndianStream(stream), i);
                    if (PacketEndPoint.IsPacketSupported(item, i) == true)
                    {
                        try
                        {
                            var packet = Activator.CreateInstance(item) as Packet;
                            if (packet is HandshakeRequest) (packet as HandshakeRequest).ProtocolVersion = (byte)i;
                            await client.SendPacketAsync(packet);
                            stream.Seek(0, SeekOrigin.Begin);
                            client.EndPoint.Stream.Context = PacketContext.Client;
                            await client.ReadPacketAsync();
                            Assert.AreEqual(stream.Position, stream.Length, item.Name);
                        }
                        catch (Exception ex)
                        {
                            Assert.Fail(item.ToString(), ex);
                        }
                    }
                }
            }
        }
    }
}
