using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using javax.crypto;
using ikvm.io;
using java.security;

namespace Pdelvo.Minecraft.Network
{
    public class RC4Stream : Stream
    {
        CipherInputStream _inputStream;
        CipherOutputStream _outputStream;

        public RC4Stream(Stream stream, byte[] key)
        {
            BaseStream = stream;
            Key = ProtocolSecurity.GenerateRS4Key(key);
        }

        static Cipher GetCipher(int mode, string algorythm, Key key)
        {
            var cipher = Cipher.getInstance(algorythm);
            cipher.init(mode, key);
            return cipher;
        }

        public Stream BaseStream { get; set; }


        Key _key;
        internal Key Key
        {
            get
            {
                return _key;
            }
            set
            {
                _inputStream = new CipherInputStream(new InputStreamWrapper(BaseStream), GetCipher(Cipher.DECRYPT_MODE, "RC4", value));
                _outputStream = new CipherOutputStream(new OutputStreamWrapper(BaseStream), GetCipher(Cipher.ENCRYPT_MODE, "RC4", value));
                _key = value;
            }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int cnt = _inputStream.read(buffer, offset, count);
            return cnt;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _outputStream.write(buffer, offset, count);
        }

        //static void RC4(byte[] bytes,int start, int length, byte[] key)
        //{
        //    byte[] s = new byte[256];
        //    byte[] k = new byte[256];
        //    byte temp;
        //    int i, j;

        //    for (i = 0; i < 256; i++)
        //    {
        //        s[i] = (byte)i;
        //        k[i] = key[i % key.GetLength(0)];
        //    }

        //    j = 0;
        //    for (i = 0; i < 256; i++)
        //    {
        //        j = (j + s[i] + k[i]) % 256;
        //        temp = s[i];
        //        s[i] = s[j];
        //        s[j] = temp;
        //    }

        //    i = j = 0;
        //    for (int x = start; x < start + length; x++)
        //    {
        //        i = (i + 1) % 256;
        //        j = (j + s[i]) % 256;
        //        temp = s[i];
        //        s[i] = s[j];
        //        s[j] = temp;
        //        int t = (s[i] + s[j]) % 256;
        //        bytes[x] ^= s[t];
        //    }
        //}
    }
}
