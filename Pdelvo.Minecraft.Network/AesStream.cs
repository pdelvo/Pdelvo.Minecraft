using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Pdelvo.Minecraft.Network
{
    public class AesStream : Stream
    {
        CryptoStream _encryptStream;
        CryptoStream _decryptStream;

        public AesStream(Stream stream, byte[] key)
        {
            BaseStream = stream;
            Key = key;
        }
        public Stream BaseStream { get; set; }

        static Rijndael GenerateAES(byte[] key)
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.Mode = CipherMode.CFB;
            cipher.Padding = PaddingMode.None;
            cipher.KeySize = 128;
            cipher.FeedbackSize = 8;
            cipher.Key = key;
            cipher.IV = key;

            return cipher;
        }

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
                var rijndael = GenerateAES(value);
                var encryptTransform = rijndael.CreateEncryptor();
                var decryptTransform = rijndael.CreateDecryptor();

                _encryptStream = new CryptoStream(BaseStream, encryptTransform, CryptoStreamMode.Write);
                _decryptStream = new CryptoStream(BaseStream, decryptTransform, CryptoStreamMode.Read);
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

        public override System.Threading.Tasks.Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            return _decryptStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            return _decryptStream.ReadByte();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _decryptStream.Read(buffer, offset, count);
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
            _encryptStream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            _decryptStream.Close();
            _encryptStream.Close();
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _decryptStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _encryptStream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return _decryptStream.EndRead(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _encryptStream.EndWrite(asyncResult);
        }
    }
}
