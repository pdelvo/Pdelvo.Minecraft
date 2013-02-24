using System;
using System.Collections.Generic;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    public class Teams : Packet
    {
        public string TeamName { get; set; }
        public byte Mode { get; set; }
        public string TeamDisplayName { get; set; }
        public string TeamPrefix { get; set; }
        public string TeamSufix { get; set; }
        public bool FriendlyFire { get; set; }
        public short PlayerCount { get; set; }
        public string[] Players { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="Teams" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public Teams()
        {
            Code = 0xD1;
        }

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

            TeamName = reader.ReadString16();
            Mode = reader.ReadByte();
            TeamDisplayName = reader.ReadString16();
            TeamPrefix = reader.ReadString16();
            FriendlyFire = reader.ReadBoolean();
            PlayerCount = reader.ReadInt16();

            var collection = new List<string> ();

            for (int i = 0; i < PlayerCount; i++)
            {
                collection.Add(reader.ReadString16 ());
            }

            Players = collection.ToArray ();
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

            writer.Write(TeamName);
            writer.Write(Mode);
            writer.Write(TeamDisplayName);
            writer.Write(TeamPrefix);
            writer.Write(FriendlyFire);
            writer.Write(PlayerCount);

            Players = Players ?? new string[0];

            foreach (var player in Players)
            {
                writer.Write(player);
            }
        }
    }
}
