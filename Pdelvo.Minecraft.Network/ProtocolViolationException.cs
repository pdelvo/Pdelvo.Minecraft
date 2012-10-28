using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Pdelvo.Minecraft.Network
{
    [Serializable]
    public class ProtocolViolationException : Exception
    {
        public ProtocolViolationException()
        {

        }
        public ProtocolViolationException(string message)
            : base(message)
        {
        }
        public ProtocolViolationException(string message, Exception ex)
            : base(message, ex)
        {
        }

        protected ProtocolViolationException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {

        }
    }
}
