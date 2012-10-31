using System;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [PacketUsage(PacketUsage.ClientToServer)]
    public class PlayerPosition : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="PlayerPosition" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public PlayerPosition()
        {
            Code = 0x0B;
        }

        /// <summary>
        ///   Gets or sets the X.
        /// </summary>
        /// <value> The X. </value>
        /// <remarks>
        /// </remarks>
        public double PositionX { get; set; }

        /// <summary>
        ///   Gets or sets the Y.
        /// </summary>
        /// <value> The Y. </value>
        /// <remarks>
        /// </remarks>
        public double PositionY { get; set; }

        /// <summary>
        ///   Gets or sets the stance.
        /// </summary>
        /// <value> The stance. </value>
        /// <remarks>
        /// </remarks>
        public double Stance { get; set; }

        /// <summary>
        ///   Gets or sets the Z.
        /// </summary>
        /// <value> The Z. </value>
        /// <remarks>
        /// </remarks>
        public double PositionZ { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether [on ground].
        /// </summary>
        /// <value> <c>true</c> if [on ground]; otherwise, <c>false</c> . </value>
        /// <remarks>
        /// </remarks>
        public bool OnGround { get; set; }

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
            PositionX = reader.ReadDouble ();
            PositionY = reader.ReadDouble ();
            Stance = reader.ReadDouble ();
            PositionZ = reader.ReadDouble ();
            OnGround = reader.ReadBoolean ();
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(Stance);
            writer.Write(PositionZ);
            writer.Write(OnGround);
        }
    }
}