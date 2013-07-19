using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    [RequireVersion(29)]
    [PacketUsage(PacketUsage.Both)]
    public class AbilityPacket : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="AbilityPacket" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public AbilityPacket()
        {
            Code = 0xCA;
        }

        public bool Flying { get; set; }
        public bool InstantBuild { get; set; }
        public bool MayFly { get; set; }
        public bool Invulnerable { get; set; }

        public float FlyingSpeed { get; set; }
        public float WalkSpeed { get; set; }


        /// <summary>
        ///   Receives the specified reader.
        /// </summary>
        /// <param name="reader"> The reader. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (version >= 32)
            {
                byte dataByte = reader.ReadByte ();
                Flying = (dataByte & 0x1) != 0;
                InstantBuild = (dataByte & 0x2) != 0;
                MayFly = (dataByte & 0x4) != 0;
                Invulnerable = (dataByte & 0x8) != 0;
                FlyingSpeed = version >= 72 ? reader.ReadSingle() : reader.ReadByte() /255f;
                WalkSpeed = version >= 72 ? reader.ReadSingle() : reader.ReadByte() / 255f;
            }
            else
            {
                Flying = reader.ReadBoolean ();
                InstantBuild = reader.ReadBoolean ();
                MayFly = reader.ReadBoolean ();
                Invulnerable = reader.ReadBoolean ();

                FlyingSpeed = version >= 72 ? 15 : 15 / 255f;
                WalkSpeed = version >= 72 ? 25 : 25 / 255f;
            }
        }

        /// <summary>
        ///   Sends the specified writer.
        /// </summary>
        /// <param name="writer"> The writer. </param>
        /// <param name="version"> The version. </param>
        /// <remarks>
        /// </remarks>
        protected override void OnSend(BigEndianStream writer, int version)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            writer.Write(Code);

            if (version >= 32)
            {
                byte d = 0;

                d |= (byte) ((Flying ? 1 : 0) << 0);
                d |= (byte) ((InstantBuild ? 1 : 0) << 1);
                d |= (byte) ((MayFly ? 1 : 0) << 2);
                d |= (byte) ((Invulnerable ? 1 : 0) << 3);
                writer.Write(d);
                if (version >= 72)
                {
                    writer.Write(FlyingSpeed);
                    writer.Write(WalkSpeed);
                }
                else
                {
                    writer.Write((byte)(FlyingSpeed * 255f));
                    writer.Write((byte)(WalkSpeed * 255f));
                }
            }
            else
            {
                writer.Write(Flying);
                writer.Write(InstantBuild);
                writer.Write(MayFly);
                writer.Write(Invulnerable);
            }
        }
    }
}