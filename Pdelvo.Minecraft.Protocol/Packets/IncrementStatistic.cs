using System;
using System.Net.NetworkInformation;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.Both)]
    public class IncrementStatistic : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="IncrementStatistic" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public IncrementStatistic()
        {
            Code = 0xC8;
        }

        /// <summary>
        ///   Gets or sets the statistic Id.
        /// </summary>
        /// <value> The statistic Id. </value>
        /// <remarks>
        /// </remarks>
        public int StatisticId { get; set; }

        /// <summary>
        ///   Gets or sets the amount.
        /// </summary>
        /// <value> The amount. </value>
        /// <remarks>
        /// </remarks>
        public int Amount { get; set; }

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
            StatisticId = reader.ReadInt32 ();
            Amount = version >=72 ? reader.ReadInt32() : reader.ReadByte ();
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
            writer.Write(StatisticId);
            if (version >= 72)
                writer.Write(Amount);
            else
                writer.Write((byte) Amount);
        }
    }
}