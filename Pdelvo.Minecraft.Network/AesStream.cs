using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Network
{
    //Thanks to _x68x for this!
    public class AesStream : Stream
    {
        private CryptoStream _decryptStream;
        private CryptoStream _encryptStream;
        private byte[] _key;

        public AesStream(Stream stream, byte[] key)
        {
            BaseStream = stream;
            Key = key;
        }

        public Stream BaseStream { get; set; }

        internal byte[] Key
        {
            get { return _key; }
            set
            {
                _key = value;
                Rijndael rijndael = GenerateAES(value);
                ICryptoTransform encryptTransform = rijndael.CreateEncryptor ();
                ICryptoTransform decryptTransform = rijndael.CreateDecryptor ();

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

        public override long Length
        {
            get { throw new NotSupportedException (); }
        }

        public override long Position
        {
            get { throw new NotSupportedException (); }
            set { throw new NotSupportedException (); }
        }

        private static Rijndael GenerateAES(byte[] key)
        {
            var cipher = new RijndaelManaged ();
            cipher.Mode = CipherMode.CFB;
            cipher.Padding = PaddingMode.None;
            cipher.KeySize = 128;
            cipher.FeedbackSize = 8;
            cipher.Key = key;
            cipher.IV = key;

            return cipher;
        }

        public override void Flush()
        {
            BaseStream.Flush ();
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _decryptStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            return _decryptStream.ReadByte ();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _decryptStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException ();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException ();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _encryptStream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            _decryptStream.Close ();
            _encryptStream.Close ();
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback,
                                               object state)
        {
            return _decryptStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback,
                                                object state)
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