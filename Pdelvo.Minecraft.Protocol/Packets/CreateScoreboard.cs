using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class CreateScoreboard : Packet
    {
        public string ScoreboardName { get; set; }
        public string ScoreboardDispayText { get; set; }

        /// <summary>
        /// 0: Create/ 1: Remove
        /// </summary>
        public byte CreateRemove { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="CreateScoreboard" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public CreateScoreboard()
        {
            Code = 0xCE;
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

            ScoreboardName = reader.ReadString16 ();
            ScoreboardDispayText = reader.ReadString16 ();
            CreateRemove = reader.ReadByte ();
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

            writer.Write(ScoreboardName);
            writer.Write(ScoreboardDispayText);
            writer.Write(CreateRemove);
        }
    }
}
