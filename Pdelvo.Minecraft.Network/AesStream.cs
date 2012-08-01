using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;

namespace Pdelvo.Minecraft.Network
{
    public class AesStream : Stream
    {
        BufferedBlockCipher _encrypter;
        BufferedBlockCipher _decrypter;

        public AesStream(Stream stream, byte[] key)
        {
            BaseStream = stream;
            Key = key;
        }
        public Stream BaseStream { get; set; }


        byte[] _key;
        internal byte[] Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                _encrypter = new BufferedBlockCipher(new CfbBlockCipher(new AesFastEngine(), 8));
                _encrypter.Init(true, new ParametersWithIV(new KeyParameter(Key), Key, 0, 16));
                _decrypter = new BufferedBlockCipher(new CfbBlockCipher(new AesFastEngine(), 8));
                _decrypter.Init(false, new ParametersWithIV(new KeyParameter(Key), Key, 0, 16));
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
            int cnt = BaseStream.Read(buffer, offset, count);

            _decrypter.ProcessBytes(buffer, offset, cnt, buffer, offset);
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
            byte[] data = new byte[count - offset];
            _encrypter.ProcessBytes(buffer, offset, count, data, 0);
            BaseStream.Write(data, 0, count);
        }
    }
}
