using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.io;
using System.IO;

namespace Pdelvo.Minecraft.Network
{
    class InputStreamWrapper : InputStream
    {
        public Stream BaseStream { get; set; }

        public InputStreamWrapper(Stream stream)
        {
            BaseStream = stream;
        }

        public override int read()
        {
            return BaseStream.ReadByte();
        }

        public override int read(byte[] b)
        {
            if (b == null) throw new ArgumentNullException("b");
            return BaseStream.Read(b, 0, b.Length);
        }

        public override void close()
        {
            BaseStream.Close();
        }

        public override int read(byte[] b, int off, int len)
        {
            if (b == null) throw new ArgumentNullException("b");
            return BaseStream.Read(b, 0, b.Length);
        }

        public override long skip(long n)
        {
            return BaseStream.Seek(n, SeekOrigin.Current);
        }
    }
}
