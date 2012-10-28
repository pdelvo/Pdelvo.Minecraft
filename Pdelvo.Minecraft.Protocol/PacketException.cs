using System;
using System.Globalization;
using System.Runtime.Serialization;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class PacketException : ProtocolViolationException
    {
        public PacketException()
        {
            Code = 0x00;
        }

        public PacketException(string message)
            :base(message)
        {

        }

        public PacketException(string message, Exception exception)
            :base(message,exception)
        {

        }

        protected PacketException(SerializationInfo info, StreamingContext context)
            :base (info, context)
        {
            if (info == null) throw new ArgumentNullException("info");
            Code = info.GetByte("Code");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <remarks></remarks>
        public PacketException(byte code)
        {
            Code = code;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        /// <remarks></remarks>
        public byte Code { get; set; }

        /// <summary>
        /// Gets the help code.
        /// </summary>
        /// <remarks></remarks>
        public string HelpCode
        {
            get { return string.Format(CultureInfo.CurrentCulture, "Packet Exception [0x{0:X2}] -  - {1}", Code, base.Message); }
        }

        /// <summary>
        /// Ruft eine Meldung ab, die die aktuelle Ausnahme beschreibt.
        /// </summary>
        /// <returns>Die Fehlermeldung, die die Ursache der Ausnahme erklärt, bzw. eine leere Zeichenfolge ("").</returns>
        /// <remarks></remarks>
        public override string Message
        {
            get { return string.Format(CultureInfo.CurrentCulture, "Packet Exception [0x{0:X2}] - {1}", Code, base.Message); }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("Code", Code);
            base.GetObjectData(info, context);
        }
    }
}