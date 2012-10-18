using System;
using System.Collections.Generic;
using System.Globalization;
using Pdelvo.Minecraft.Protocol.Packets;

namespace Pdelvo.Minecraft.Protocol.Helper
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public static class ProtocolHelper
    {
        /// <summary>
        /// 
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
                                                                                        {46, "12w41a"}
                                                                                    };

        /// <summary>
        /// Registers the general packets.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        /// <remarks></remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification="This is neccecary")]
        public static void RegisterGeneralPackets(PacketEndPoint endPoint)
        {
            if (endPoint == null) throw new ArgumentNullException("endPoint");
            endPoint.RegisterPacket<KeepAlive>(0x00);
            endPoint.RegisterPacket<ChatPacket>(0x03);
            endPoint.RegisterPacket<EntityEquipment>(0x05);
            endPoint.RegisterPacket<Respawn>(0x09);
            endPoint.RegisterPacket<PlayerDigging>(0x0E);
            endPoint.RegisterPacket<PlayerBlockPlacement>(0x0F);
            endPoint.RegisterPacket<HoldingChange>(0x10);
            endPoint.RegisterPacket<Animation>(0x12);
            endPoint.RegisterPacket<EntityAction>(0x13);
            endPoint.RegisterPacket<PickupSpawn>(0x15);
            endPoint.RegisterPacket<EntityPainting>(0x19);
            endPoint.RegisterPacket<StanceUpdate>(0x1B);
            endPoint.RegisterPacket<EntityVelocity>(0x1C);
            endPoint.RegisterPacket<EntityHeadLook>(0x23);
            endPoint.RegisterPacket<AttachEntity>(0x27);
            endPoint.RegisterPacket<EntityMetadata>(0x28);
            endPoint.RegisterPacket<EntityEffect>(0x29);
            endPoint.RegisterPacket<RemoveEntityEffect>(0x2A);
            endPoint.RegisterPacket<Experience>(0x2B);
            endPoint.RegisterPacket<BlockChange>(0x35);
            endPoint.RegisterPacket<Explosion>(0x3C);
            endPoint.RegisterPacket<SoundEffect>(0x3D);
            endPoint.RegisterPacket<InvalidState>(0x46);
            endPoint.RegisterPacket<OpenWindow>(0x64);
            endPoint.RegisterPacket<CloseWindow>(0x65);
            endPoint.RegisterPacket<Transaction>(0x6A);
            endPoint.RegisterPacket<CreativeInventoryAction>(0x6B);
            endPoint.RegisterPacket<EnchantItem>(0x6C);
            endPoint.RegisterPacket<UpdateSign>(0x82);
            endPoint.RegisterPacket<AbilityPacket>(0xCA);
            endPoint.RegisterPacket<AutoCompletion>(0xCB);
            endPoint.RegisterPacket<IncrementStatistic>(0xC8);
            endPoint.RegisterPacket<PlayerListItem>(0xC9);
            endPoint.RegisterPacket<PluginMessage>(0xFA);
            endPoint.RegisterPacket<DisconnectPacket>(0xFF);
        }

        /// <summary>
        /// Builds the mot D string in minecraft version pre 12w42b
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="usedSlots">The used slots.</param>
        /// <param name="maxSlots">The max slots.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string BuildMotDString(string message, int usedSlots, int maxSlots)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}§{1}§{2}", message, usedSlots, maxSlots);
        }

        /// <summary>
        /// Builds the mot D string in minecraft version pre 12w42b
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="usedSlots">The used slots.</param>
        /// <param name="maxSlots">The max slots.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string BuildMotDString(byte protocolVersion, string versionString, string message, int usedSlots, int maxSlots)
        {
            return string.Format(CultureInfo.InvariantCulture, "§1\0{0}\0{1}\0{2}\0{3}\0{4}", protocolVersion, versionString, message, usedSlots, maxSlots);
        }

        /// <summary>
        /// Builds the mot D packet.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="usedSlots">The used slots.</param>
        /// <param name="maxSlots">The max slots.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Packet BuildMotDPacket(string message, int usedSlots, int maxSlots)
        {
            return new DisconnectPacket { Reason = BuildMotDString(message, usedSlots, maxSlots) };
        }

        /// <summary>
        /// Builds the mot D packet in minecraft version pre 12w42b
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="usedSlots">The used slots.</param>
        /// <param name="maxSlots">The max slots.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Packet BuildMotDPacket(byte protocolVersion, string versionString, string message, int usedSlots, int maxSlots)
        {
            return new DisconnectPacket { Reason = BuildMotDString(protocolVersion, versionString, message, usedSlots, maxSlots) };
        }

        /// <summary>
        /// Gets the name of the minecraft version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetMinecraftVersionName(int version)
        {
            if (MinecraftVersionNames.ContainsKey(version)) return MinecraftVersionNames[version];
            else return "Unknown version: " + version;
        }
    }
}