using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.ServerToClient)]
    public class UpdateHealth : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="UpdateHealth" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public UpdateHealth()
        {
            Code = 0x08;
        }

        /// <summary>
        ///   Gets or sets the health.
        /// </summary>
        /// <value> The health. </value>
        /// <remarks>
        /// </remarks>
        public short Health { get; set; }

        /// <summary>
        ///   Gets or sets the food.
        /// </summary>
        /// <value> The food. </value>
        /// <remarks>
        /// </remarks>
        public short Food { get; set; }

        /// <summary>
        ///   Gets or sets the food saturation.
        /// </summary>
        /// <value> The food saturation. </value>
        /// <remarks>
        /// </remarks>
        public float FoodSaturation { get; set; }

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
            Health = reader.ReadInt16 ();
            Food = reader.ReadInt16 ();
            FoodSaturation = reader.ReadSingle ();
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
            writer.Write(Health);
            writer.Write(Food);
            writer.Write(FoodSaturation);
        }
    }
}