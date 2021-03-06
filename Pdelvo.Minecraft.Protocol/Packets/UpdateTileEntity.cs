using System;
using System.Collections.Generic;
using System.Linq;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// </remarks>
    [RequireVersion(25)]
    [PacketUsage(PacketUsage.ServerToClient)]
    public class UpdateTileEntity : Packet
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="UpdateTileEntity" /> class.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public UpdateTileEntity()
        {
            Code = 0x84;

            DataNBT = Enumerable.Empty<byte> ();
        }

        public override bool CanBeDelayed
        {
            get { return true; }
        }

        /// <summary>
        ///   Gets or sets the X.
        /// </summary>
        /// <value> The X. </value>
        /// <remarks>
        /// </remarks>
        public int PositionX { get; set; }

        /// <summary>
        ///   Gets or sets the Y.
        /// </summary>
        /// <value> The Y. </value>
        /// <remarks>
        /// </remarks>
        public short PositionY { get; set; }

        /// <summary>
        ///   Gets or sets the Z.
        /// </summary>
        /// <value> The Z. </value>
        /// <remarks>
        /// </remarks>
        public int PositionZ { get; set; }

        /// <summary>
        ///   Gets or sets the action.
        /// </summary>
        /// <value> The action. </value>
        /// <remarks>
        /// </remarks>
        public byte Action { get; set; }

        /// <summary>
        ///   Gets or sets the custom1.
        /// </summary>
        /// <value> The custom1. </value>
        /// <remarks>
        /// </remarks>
        public int Custom1 { get; set; }

        /// <summary>
        ///   Gets or sets the custom2.
        /// </summary>
        /// <value> The custom2. </value>
        /// <remarks>
        /// </remarks>
        public int Custom2 { get; set; }

        /// <summary>
        ///   Gets or sets the custom3.
        /// </summary>
        /// <value> The custom3. </value>
        /// <remarks>
        /// </remarks>
        public int Custom3 { get; set; }

        public IEnumerable<byte> DataNBT { get; set; }

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
            PositionX = reader.ReadInt32 ();
            PositionY = reader.ReadInt16 ();
            PositionZ = reader.ReadInt32 ();
            Action = reader.ReadByte ();
            if (version >= 39)
            {
                short length = reader.ReadInt16 ();
                length = length == -1 ? (short) 0 : length;
                DataNBT = reader.ReadBytes(length);
            }
            else
            {
                Custom1 = reader.ReadInt32 ();
                Custom2 = reader.ReadInt32 ();
                Custom3 = reader.ReadInt32 ();
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
            writer.Write(PositionX);
            writer.Write(PositionY);
            writer.Write(PositionZ);
            writer.Write(Action);
            if (version >= 39)
            {
                byte[] buf = DataNBT.ToArray ();
                writer.Write((short) (buf.Length == 0 ? -1 : buf.Length));
                writer.Write(buf);
            }
            else
            {
                writer.Write(Custom1);
                writer.Write(Custom2);
                writer.Write(Custom3);
            }
        }
    }
}