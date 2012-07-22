using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Pdelvo.Minecraft.Network
{
    public class FullyReadStream : Stream
    {
        private readonly byte[] _readAheadBuffer;
        private Stream _sourceStream;
        private long _pos; // pseudo-position
        private int _readAheadLength;
        private int _readAheadOffset;
        private List<byte> _buffer = new List<byte>();

        public FullyReadStream(Stream sourceStream)
        {
            _sourceStream = sourceStream;
            _readAheadBuffer = new byte[1024*1024];
        }

        public Stream BaseStream
        {
            get { return _sourceStream; }
            set { _sourceStream = value; }
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
            get { return false; }
        }

        public override long Length
        {
            get { return _pos; }
        }

        public override long Position
        {
            get { return _pos; }
            set { throw new InvalidOperationException(); }
        }

        public override void Flush()
        {
            _sourceStream.Write(_buffer.ToArray(), 0, _buffer.Count);
            _sourceStream.Flush();
            _buffer.Clear();
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            await _sourceStream.WriteAsync(_buffer.ToArray(), 0, _buffer.Count);
            await _sourceStream.FlushAsync();
            _buffer.Clear();
        }

        [DebuggerStepThrough]
        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = 0;
            while (bytesRead < count)
            {
                int readAheadAvailableBytes = _readAheadLength - _readAheadOffset;

                if (readAheadAvailableBytes > 0)
                {
                    int bytesRequired = count - bytesRead;
                    int toCopy = Math.Min(readAheadAvailableBytes, bytesRequired);
                    //Array.Copy(readAheadBuffer, readAheadOffset, buffer, offset + bytesRead, toCopy);
                    Buffer.BlockCopy(_readAheadBuffer, _readAheadOffset, buffer, offset + bytesRead, toCopy);
                    bytesRead += toCopy;
                    _readAheadOffset += toCopy;
                }
                else
                {
                    ReadData(count);
                }
            }
            _pos += bytesRead;
            return bytesRead;
        }

        [DebuggerStepThrough]
        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken token)
        {
            int bytesRead = 0;
            while (bytesRead < count)
            {
                int readAheadAvailableBytes = _readAheadLength - _readAheadOffset;

                if (readAheadAvailableBytes > 0)
                {
                    int bytesRequired = count - bytesRead;
                    int toCopy = Math.Min(readAheadAvailableBytes, bytesRequired);
                    //Array.Copy(readAheadBuffer, readAheadOffset, buffer, offset + bytesRead, toCopy);
                    Buffer.BlockCopy(_readAheadBuffer, _readAheadOffset, buffer, offset + bytesRead, toCopy);
                    bytesRead += toCopy;
                    _readAheadOffset += toCopy;
                }
                else
                {
                    await ReadDataAsync(count, token);
                }
            }
            _pos += bytesRead;
            return bytesRead;
            //return base.ReadAsync(buffer, offset, count, cancellationToken);
        }

        private void ReadData(int maxCount)
        {
            _readAheadOffset = 0;
            _readAheadLength = _sourceStream.Read(_readAheadBuffer, 0, maxCount);
            if (_readAheadLength == 0)
                throw new EndOfStreamException();
        }

        private Task ReadDataAsync(int maxCount)
        {
            return ReadDataAsync(maxCount, CancellationToken.None);
        }

        private async Task ReadDataAsync(int maxCount, CancellationToken token)
        {
            _readAheadOffset = 0;
            _readAheadLength = await _sourceStream.ReadAsync(_readAheadBuffer, 0, maxCount, token);
            if (_readAheadLength == 0)
                throw new EndOfStreamException();
        }

        [DebuggerStepThrough]
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (offset == 0 && origin == SeekOrigin.Begin)
            {
                _sourceStream.Seek(offset, origin);
                _readAheadOffset = 0;
                _readAheadLength = 0;
                return 0;
            }
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            //_sourceStream.Write(buffer, offset, count);
             _buffer.AddRange(buffer.Skip(offset).Take(count));
        }

        public override void WriteByte(byte value)
        {
            //_sourceStream.WriteByte(value);
            _buffer.Add(value);
        }

        protected override void Dispose(bool disposing)
        {
            _sourceStream.Dispose();
            base.Dispose(disposing);
        }

        public int Peek()
        {
            if (_readAheadLength - _readAheadOffset == 0)
                ReadData(1);
            return _readAheadBuffer[_readAheadOffset];
        }

        public Task<int> PeekAsync()
        {
            return PeekAsync(CancellationToken.None);
        }


        public async Task<int> PeekAsync(CancellationToken token)
        {
            if (_readAheadLength - _readAheadOffset == 0)
                await ReadDataAsync(1, token);
            return _readAheadBuffer[_readAheadOffset];
        }
    }
}