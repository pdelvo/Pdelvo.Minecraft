using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.io;
using System.IO;

namespace Pdelvo.Minecraft.Network
{
    class OutputStreamWrapper : OutputStream
    {
        public Stream BaseStream { get; set; }

        public OutputStreamWrapper(Stream stream)
        {
            BaseStream = stream;
        }

        public override void write(int i)
        {
            BaseStream.WriteByte((byte)i);
        }
        public override void write(byte[] b)
        {
            if (b == null) throw new ArgumentNullException("b");
            BaseStream.Write(b, 0, b.Length);
        }

        public override void write(byte[] b, int off, int len)
        {
            BaseStream.Write(b, off, len);
        }

        public override void close()
        {
            BaseStream.Close();
        }

        public override void flush()
        {
            BaseStream.Flush();
        }
    }
}
