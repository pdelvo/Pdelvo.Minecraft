using Pdelvo.Minecraft.Network;
using System;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [PacketUsage(PacketUsage.ClientToServer)]
    public class PlayerListPing : Packet
    {
        public byte MagicByte { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerListPing"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PlayerListPing()
        {
            Code = 0xFE;
            MagicByte = 1;
        }

        /// <summary>
        /// Receives the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnReceive(BigEndianStream reader, int version)
        {
            if (reader == null)
                throw new System.ArgumentNullException("reader");
            try
            {
                try
                {
                    reader.ReadTimeout = 1;
                }
                catch (InvalidOperationException) { }
                MagicByte = reader.ReadByte();
            }
            catch (Exception)
            {
                MagicByte = 0;
            }
        }

        /// <summary>
        /// Sends the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="version">The version.</param>
        /// <remarks></remarks>
        protected override void OnSend(BigEndianStream writer, int version)
        {
            if (writer == null)
                throw new System.ArgumentNullException("writer");
            writer.Write(Code);

            if (version >= 47) 
                writer.Write(MagicByte);
        }
    }
}