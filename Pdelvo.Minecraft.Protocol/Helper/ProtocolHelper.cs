using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Helper
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class ProtocolHelper
    {
        /// <summary>
        /// </summary>
        private static readonly Dictionary<int, string> MinecraftVersionNames = new Dictionary<int, string>
                                                                                    {
                                                                                        {22, "1.0.0"},
                                                                                        {23, "1.1.0"},
                                                                                        {24, "12w01-05a"},
                                                                                        {25, "12w06a"},
                                                                                        {27, "12w07a"},
                                                                                        {28, "1.2.3"},
                                                                                        {29, "1.2.4/1.2.5"},
                                                                                        {30, "12w16a"},
                                                                                        {31, "12w17a"},
                                                                                        {32, "12w18a/12w19a"},
                                                                                        {33, "12w21ab"},
                                                                                        {34, "12w22a"},
                                                                                        {35, "12w23a"},
                                                                                        {36, "12w24a"},
                                                                                        {37, "12w25/26a"},
                                                                                        {38, "12w27a"},
                                                                                        {39, "1.3"},
                                                                                        {40, "12w32a"},
                                                                                        {41, "12w34a"},
                                                                                        {42, "12w34b"},
                                                                                        {45, "12w40a"},
                                                                                        {46, "12w41a"},
                                                                                        {47, "1.4"},
                                                                                        {48, "1.4.3"},
                                                                                        {49, "1.4.5"},
                                                                                        {50, "12w49a"},
                                                                                        {51, "1.4.6"}
                                                                                    };

        public static void RegisterPackets(PacketEndPoint endPoint, PacketUsage packetUsage)
        {
            Assembly assembly = Assembly.GetExecutingAssembly ();

            IEnumerable<Type> possiblePackets =
                assembly.GetTypes ().Where(a => a.GetCustomAttributes<PacketUsageAttribute> ().Any());

            foreach (Type item in possiblePackets)
            {
                var usage = item.GetCustomAttribute<PacketUsageAttribute> ();

                if ((usage.PacketUsage & packetUsage) == packetUsage)
                {
                    var packet = (Packet) Activator.CreateInstance(item);

                    endPoint.RegisterPacket(packet.Code, item);
                }
            }
        }

        /// <summary>
        ///   Builds the mot D string in minecraft version pre 12w42b
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="usedSlots"> The used slots. </param>
        /// <param name="maxSlots"> The max slots. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static string BuildMotDString(string message, int usedSlots, int maxSlots)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}§{1}§{2}", message, usedSlots, maxSlots);
        }

        /// <summary>
        ///   Builds the mot D string in minecraft version pre 12w42b
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="usedSlots"> The used slots. </param>
        /// <param name="maxSlots"> The max slots. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static string BuildMotDString(byte protocolVersion, string versionString, string message, int usedSlots,
                                             int maxSlots)
        {
            return string.Format(CultureInfo.InvariantCulture, "§1\0{0}\0{1}\0{2}\0{3}\0{4}", protocolVersion,
                                 versionString, message, usedSlots, maxSlots);
        }

        /// <summary>
        ///   Builds the mot D packet.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="usedSlots"> The used slots. </param>
        /// <param name="maxSlots"> The max slots. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static Packet BuildMotDPacket(string message, int usedSlots, int maxSlots)
        {
            return new DisconnectPacket {Reason = BuildMotDString(message, usedSlots, maxSlots)};
        }

        /// <summary>
        ///   Builds the mot D packet in minecraft version pre 12w42b
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="usedSlots"> The used slots. </param>
        /// <param name="maxSlots"> The max slots. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static Packet BuildMotDPacket(byte protocolVersion, string versionString, string message, int usedSlots,
                                             int maxSlots)
        {
            return new DisconnectPacket
                       {Reason = BuildMotDString(protocolVersion, versionString, message, usedSlots, maxSlots)};
        }

        /// <summary>
        ///   Gets the name of the minecraft version.
        /// </summary>
        /// <param name="version"> The version. </param>
        /// <returns> </returns>
        /// <remarks>
        /// </remarks>
        public static string GetMinecraftVersionName(int version)
        {
            if (MinecraftVersionNames.ContainsKey(version)) return MinecraftVersionNames[version];
            else return "Unknown version: " + version;
        }
    }
}