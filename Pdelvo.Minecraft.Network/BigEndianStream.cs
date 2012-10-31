using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Network
{
    public class BigEndianStream : Stream
    {
        private readonly MemoryStream _bufferStream = new MemoryStream ();

        public BigEndianStream(Stream stream)
        {
            Net = stream;
            Context = PacketContext.None;
        }

        public PacketContext Context { get; set; }
        public object Owner { get; set; }

        public Stream Net { get; set; }

        public override bool CanRead
        {
            get { return Net.CanRead; }
        }

        public override bool CanSeek
        {
            get { return Net.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return Net.CanWrite; }
        }

        public override long Length
        {
            get { return Net.Length; }
        }

        public override long Position
        {
            get { return Net.Position; }
            set { Net.Position = value; }
        }

        public bool BufferEnabled { get; set; }

        public override int ReadTimeout
        {
            get { return Net.ReadTimeout; }
            set { Net.ReadTimeout = value; }
        }

        public override int WriteTimeout
        {
            get { return Net.WriteTimeout; }
            set { Net.WriteTimeout = value; }
        }

        [DebuggerStepThrough]
        public new byte ReadByte()
        {
            int b = Net.ReadByte ();
            if (b >= byte.MinValue && b <= byte.MaxValue)
            {
                _bufferStream.WriteByte((byte) b);
                return (byte) b;
            }
            throw new EndOfStreamException ();
        }

        public async Task<byte> ReadByteAsync()
        {
            int b = await Net.ReadByteAsync ();
            if (b >= byte.MinValue && b <= byte.MaxValue)
            {
                _bufferStream.WriteByte((byte) b);
                return (byte) b;
            }
            throw new EndOfStreamException ();
        }

        public byte[] GetBuffer()
        {
            byte[] b = _bufferStream.ToArray ();
            _bufferStream.SetLength(0);
            return b;
        }

        public byte[] ReadBytes(int count)
        {
            var input = new byte[count];

            Read(input, 0, count);
            return (input);
        }

        public async Task<byte[]> ReadBytesAsync(int count)
        {
            var input = new byte[count];

            await ReadAsync(input, 0, count);
            return (input);
        }

        public short ReadInt16()
        {
            return unchecked((short) ((ReadByte () << 8) | ReadByte ()));
        }

        public async Task<short> ReadInt16Async()
        {
            return unchecked((short) (((await ReadByteAsync ()) << 8) | (await ReadByteAsync ())));
        }

        public int ReadInt32()
        {
            return unchecked((ReadByte () << 24) | (ReadByte () << 16) | (ReadByte () << 8) | ReadByte ());
        }

        public async Task<int> ReadInt32Async()
        {
            return unchecked(((await ReadByteAsync ()) << 24) | ((await ReadByteAsync ()) << 16)
                             | ((await ReadByteAsync ()) << 8) | (await ReadByteAsync ()));
        }

        public long ReadInt64()
        {
            unchecked
            {
                var l = new byte[8];
                if (Read(l, 0, l.Length) != 8)
                    throw new EndOfStreamException ();

                long p = 0;
                p |= (long) l[0] << 56;
                p |= (long) l[1] << 48;
                p |= (long) l[2] << 40;
                p |= (long) l[3] << 32;
                p |= (long) l[4] << 24;
                p |= (long) l[5] << 16;
                p |= (long) l[6] << 8;
                p |= l[7];
                return p;
            }
        }

        public async Task<long> ReadInt64Async()
        {
            unchecked
            {
                var l = new byte[8];
                if (await ReadAsync(l, 0, l.Length) != 8)
                    throw new EndOfStreamException ();

                long p = 0;
                p |= (long) l[0] << 56;
                p |= (long) l[1] << 48;
                p |= (long) l[2] << 40;
                p |= (long) l[3] << 32;
                p |= (long) l[4] << 24;
                p |= (long) l[5] << 16;
                p |= (long) l[6] << 8;
                p |= l[7];
                return p;
            }
        }

        public unsafe float ReadSingle()
        {
            int i = ReadInt32 ();
            return *(float*) &i;
        }

        public async Task<float> ReadSingleAsync()
        {
            int i = await ReadInt32Async ();
            unsafe
            {
                return *(float*) &i;
            }
        }

        public double ReadDouble()
        {
            var r = new byte[8];
            for (int i = 7; i >= 0; i--)
            {
                r[i] = ReadByte ();
            }
            return BitConverter.ToDouble(r, 0);
        }

        public async Task<double> ReadDoubleAsync()
        {
            var r = new byte[8];
            for (int i = 7; i >= 0; i--)
            {
                r[i] = await ReadByteAsync ();
            }
            return BitConverter.ToDouble(r, 0);
        }

        public string ReadString16()
        {
            int len = ReadInt16 ();
            if (len < 0)
            {
                throw new ProtocolViolationException("String length less then zero");
            }
            byte[] b = ReadBytes(len*2);
            return Encoding.BigEndianUnicode.GetString(b);
        }

        public async Task<string> ReadString16Async()
        {
            int len = await ReadInt16Async ();
            if (len < 0)
            {
                throw new ProtocolViolationException("String length less then zero");
            }
            byte[] b = await ReadBytesAsync(len*2);
            return Encoding.BigEndianUnicode.GetString(b);
        }

        public string ReadString8()
        {
            int len = ReadInt16 ();
            byte[] b = ReadBytes(len);
            return Encoding.UTF8.GetString(b);
        }

        public async Task<string> ReadString8Async()
        {
            int len = await ReadInt16Async ();
            byte[] b = await ReadBytesAsync(len);
            return Encoding.UTF8.GetString(b);
        }

        public bool ReadBoolean()
        {
            return ReadByte () == 1;
        }

        public async Task<bool> ReadBooleanAsync()
        {
            return await ReadByteAsync () == 1;
        }

        public void Write(byte data)
        {
            Net.WriteByte(data);
        }

        public Task WriteAsync(byte data)
        {
            return Net.WriteByteAsync(data);
        }

        public void Write(short data)
        {
            Write(unchecked((byte) (data >> 8)));
            Write(unchecked((byte) data));
        }

        public async Task WriteAsync(short data)
        {
            await WriteAsync(unchecked((byte) (data >> 8)));
            await WriteAsync(unchecked((byte) data));
        }

        public void Write(int data)
        {
            Write(unchecked((byte) (data >> 24)));
            Write(unchecked((byte) (data >> 16)));
            Write(unchecked((byte) (data >> 8)));
            Write(unchecked((byte) data));
        }

        public async Task WriteAsync(int data)
        {
            await WriteAsync(unchecked((byte) (data >> 24)));
            await WriteAsync(unchecked((byte) (data >> 16)));
            await WriteAsync(unchecked((byte) (data >> 8)));
            await WriteAsync(unchecked((byte) data));
        }

        public void Write(long data)
        {
            Write(unchecked((byte) (data >> 56)));
            Write(unchecked((byte) (data >> 48)));
            Write(unchecked((byte) (data >> 40)));
            Write(unchecked((byte) (data >> 32)));
            Write(unchecked((byte) (data >> 24)));
            Write(unchecked((byte) (data >> 16)));
            Write(unchecked((byte) (data >> 8)));
            Write(unchecked((byte) data));
        }

        public async Task WriteAsync(long data)
        {
            await WriteAsync(unchecked((byte) (data >> 56)));
            await WriteAsync(unchecked((byte) (data >> 48)));
            await WriteAsync(unchecked((byte) (data >> 40)));
            await WriteAsync(unchecked((byte) (data >> 32)));
            await WriteAsync(unchecked((byte) (data >> 24)));
            await WriteAsync(unchecked((byte) (data >> 16)));
            await WriteAsync(unchecked((byte) (data >> 8)));
            await WriteAsync(unchecked((byte) data));
        }

        public unsafe void Write(float data)
        {
            Write(*(int*) &data);
        }

        public Task WriteAsync(float data)
        {
            int i;
            unsafe
            {
                i = *(int*) &data;
            }
            return WriteAsync(i);
        }

        public unsafe void Write(double data)
        {
            Write(*(long*) &data);
        }

        public Task WriteAsync(double data)
        {
            long i;
            unsafe
            {
                i = *(long*) &data;
            }
            return WriteAsync(i);
        }

        public void Write(string data)
        {
            byte[] b = Encoding.BigEndianUnicode.GetBytes(data ?? "");
            Write((short) (data ?? "").Length);
            Write(b, 0, b.Length);
        }

        public async Task WriteAsync(string data)
        {
            byte[] b = Encoding.BigEndianUnicode.GetBytes(data ?? "");
            await WriteAsync((short) (data ?? "").Length);
            await WriteAsync(b, 0, b.Length);
        }

        public void Write(byte[] data)
        {
            if (data != null)
                Write(data, 0, data.Length);
        }

        public async Task WriteAsync(byte[] data)
        {
            if (data != null)
                await WriteAsync(data, 0, data.Length);
        }

        public void Write8(string data)
        {
            byte[] b = Encoding.UTF8.GetBytes(data ?? "");
            Write((short) b.Length);
            Write(b, 0, b.Length);
        }

        public async Task Write8Async(string data)
        {
            byte[] b = Encoding.UTF8.GetBytes(data ?? "");
            await WriteAsync((short) b.Length);
            await WriteAsync(b, 0, b.Length);
        }

        public void Write(bool data)
        {
            Write((byte) (data ? 1 : 0));
        }

        public Task WriteAsync(bool data)
        {
            return WriteAsync((byte) (data ? 1 : 0));
        }

        public override void Flush()
        {
            Net.Flush ();
        }

        public override Task FlushAsync(CancellationToken token)
        {
            return Net.FlushAsync(token);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int cnt = Net.Read(buffer, offset, count);

            _bufferStream.Write(buffer, 0, cnt);
            return cnt;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count,
                                                  CancellationToken cancellationToken)
        {
            int cnt = await Net.ReadAsync(buffer, offset, count, cancellationToken);

            _bufferStream.Write(buffer, 0, cnt);
            return cnt;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return Net.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            Net.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Net.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken token)
        {
            return Net.WriteAsync(buffer, offset, count, token);
        }

        public double ReadDoublePacked()
        {
            return ReadInt32 ()/32.0;
        }

        public async Task<double> ReadDoublePackedAsync()
        {
            return await ReadInt32Async ()/32.0;
        }

        public void WriteDoublePacked(double value)
        {
            Write((int) (value*32.0));
        }

        public Task WriteDoublePackedAsync(double value)
        {
            return WriteAsync((int) (value*32.0));
        }

        public override void Close()
        {
            Net.Close ();
        }

        public int Peek()
        {
            if (Net is FullyReadStream)
                return ((FullyReadStream) Net).Peek ();
            else
            {
                int i = Net.ReadByte ();
                Net.Seek(Net.Position - 1, SeekOrigin.Begin);
                return i;
            }
        }

        public async Task<int> PeekAsync()
        {
            if (Net is FullyReadStream)
                return await ((FullyReadStream) Net).PeekAsync ();
            else
            {
                int i = await Net.ReadByteAsync ();
                Net.Seek(Net.Position - 1, SeekOrigin.Begin);
                return i;
            }
        }
    }
}