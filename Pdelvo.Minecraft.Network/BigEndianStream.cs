using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Pdelvo.Minecraft.Network
{
    public class BigEndianStream : Stream
    {
        public PacketContext Context { get; set; }
        public object Owner { get; set; }

        private readonly MemoryStream _bufferStream = new MemoryStream();

        public BigEndianStream(Stream stream)
        {
            Net = stream;
            Context = PacketContext.None;
        }

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
        [DebuggerStepThrough]
        public new byte ReadByte()
        {
            int b = Net.ReadByte();


            if (b >= byte.MinValue && b <= byte.MaxValue)
            {
                //if (BufferEnabled)
                    _bufferStream.WriteByte((byte) b);
                return (byte) b;
            }
            throw new EndOfStreamException();
        }

        public byte[] GetBuffer()
        {
            //if (!BufferEnabled)
            //    throw new InvalidOperationException("BufferEnabled must be set to true");
            byte[] b = _bufferStream.ToArray();
            _bufferStream.SetLength(0);
            return b;
        }

        public byte[] ReadBytes(int count)
        {
            var input = new byte[count];

            Read(input, 0, count);
            return (input);
        }

        //public sbyte ReadSByte()
        //{
        //    return unchecked((sbyte) ReadByte());
        //}

        public short ReadInt16()
        {
            return unchecked((short) ((ReadByte() << 8) | ReadByte()));
        }

        public int ReadInt32()
        {
            return unchecked((ReadByte() << 24) | (ReadByte() << 16) | (ReadByte() << 8) | ReadByte());
        }

        public long ReadInt64()
        {
            return unchecked((ReadByte() << 56) | (ReadByte() << 48) | (ReadByte() << 40) | (ReadByte() << 32)
                             | (ReadByte() << 24) | (ReadByte() << 16) | (ReadByte() << 8) | ReadByte());
        }

        public unsafe float ReadSingle()
        {
            int i = ReadInt32();
            return *(float*) &i;
        }

        public double ReadDouble()
        {
            var r = new byte[8];
            for (int i = 7; i >= 0; i--)
            {
                r[i] = ReadByte();
            }
            return BitConverter.ToDouble(r, 0);
        }

        public string ReadString16()
        {
            int len = ReadInt16();
            //if (len > maxLen)
            //    throw new IOException("String field too long");
            if (len < 0)
            {
                throw new ProtocolViolationException("String length less then zero");
            }
            byte[] b = ReadBytes(len*2);
            return Encoding.BigEndianUnicode.GetString(b);
        }

        public string ReadString8()
        {
            int len = ReadInt16();
            //if (len > maxLen)
            //    throw new IOException("String field too long");
            byte[] b = ReadBytes(len);
            return Encoding.UTF8.GetString(b);
        }

        public bool ReadBoolean()
        {
            return ReadByte() == 1;
        }

        public void Write(byte data)
        {
            Net.WriteByte(data);
        }

        //public void Write(sbyte data)
        //{
        //    Write(unchecked((byte) data));
        //}

        public void Write(short data)
        {
            Write(unchecked((byte) (data >> 8)));
            Write(unchecked((byte) data));
        }

        public void Write(int data)
        {
            Write(unchecked((byte) (data >> 24)));
            Write(unchecked((byte) (data >> 16)));
            Write(unchecked((byte) (data >> 8)));
            Write(unchecked((byte) data));
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

        public unsafe void Write(float data)
        {
            Write(*(int*) &data);
        }

        public unsafe void Write(double data)
        {
            Write(*(long*) &data);
        }

        public void Write(string data)
        {
            byte[] b = Encoding.BigEndianUnicode.GetBytes(data ?? "");
            Write((short) (data ?? "").Length);
            Write(b, 0, b.Length);
        }

        public void Write(byte[] data)
        {
            if (data != null)
                Write(data, 0, data.Length);
        }

        public void Write8(string data)
        {
            byte[] b = Encoding.UTF8.GetBytes(data ?? "");
            Write((short) b.Length);
            Write(b, 0, b.Length);
        }

        public void Write(bool data)
        {
            Write((byte) (data ? 1 : 0));
        }

        public override void Flush()
        {
            Net.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int cnt = Net.Read(buffer, offset, count);

            //if (BufferEnabled)
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

        public double ReadDoublePacked()
        {
            return ReadInt32()/32.0;
        }

        public void WriteDoublePacked(double value)
        {
            Write((int)(value * 32.0));
        }

        public override void Close()
        {
            Net.Close();
        }

        public int Peek()
        {
            if (Net is FullyReadStream)
                return ((FullyReadStream)Net).Peek();
            else
            {
                int i = Net.ReadByte();
                Net.Seek(Net.Position -1, SeekOrigin.Begin);
                return i;
            }
        }
    }
}