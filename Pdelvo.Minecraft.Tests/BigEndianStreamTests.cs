using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Tests
{
    [TestClass]
    public class BigEndianStreamTests
    {
        [TestMethod]
        public void StringTest()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            string testString = "Hello, World!";

            bigEndianStream.Write(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            var result = bigEndianStream.ReadString16();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);


            memStream.SetLength(0);

            bigEndianStream.Write8(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            result = bigEndianStream.ReadString8();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task StringTestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            string testString = "Hello, World!";

            await bigEndianStream.WriteAsync(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            var result = await bigEndianStream.ReadString16Async();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);


            memStream.SetLength(0);

            bigEndianStream.Write8(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            result = await bigEndianStream.ReadString8Async();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void ByteTest()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = byte.MinValue;
            var testByte2 = byte.MaxValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadByte(), testByte1);
            Assert.AreEqual(bigEndianStream.ReadByte(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task ByteTestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = byte.MinValue;
            var testByte2 = byte.MaxValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadByteAsync(), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadByteAsync(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void Int16Test()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = short.MaxValue;
            var testByte2 = short.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadInt16(), testByte1);
            Assert.AreEqual(bigEndianStream.ReadInt16(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task Int16TestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = short.MaxValue;
            var testByte2 = short.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadInt16Async(), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadInt16Async(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void Int32Test()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = int.MaxValue;
            var testByte2 = int.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadInt32(), testByte1);
            Assert.AreEqual(bigEndianStream.ReadInt32(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task Int32TestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = int.MaxValue;
            var testByte2 = int.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadInt32Async(), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadInt32Async(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void Int64Test()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = long.MaxValue;
            var testByte2 = long.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadInt64(), testByte1);
            Assert.AreEqual(bigEndianStream.ReadInt64(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task Int64TestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = long.MaxValue;
            var testByte2 = long.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadInt64Async(), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadInt64Async(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task DoubleTest()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = double.MaxValue;
            var testByte2 = double.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadDoubleAsync(), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadDoubleAsync(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void SingleTest()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = float.MaxValue;
            var testByte2 = float.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadSingle(), testByte1);
            Assert.AreEqual(bigEndianStream.ReadSingle(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task SingleTestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = float.MaxValue;
            var testByte2 = float.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadSingleAsync(), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadSingleAsync(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void BooleanTest()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = true;
            var testByte2 = false;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadBoolean(), testByte1);
            Assert.AreEqual(bigEndianStream.ReadBoolean(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task BooleanTestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testByte1 = true;
            var testByte2 = false;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadBooleanAsync(), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadBooleanAsync(), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void BytesTest()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testBytes = new byte[1024];

            new Random().NextBytes(testBytes);

            bigEndianStream.Write(testBytes);

            memStream.Seek(0, SeekOrigin.Begin);

            var result = bigEndianStream.ReadBytes(1024);

            for (int i = 0; i < 1024; i++)
            {
                Assert.AreEqual(testBytes[i], result[i]);
            }
        }

        [TestMethod]
        public async Task BytesTestAsync()
        {
            MemoryStream memStream = new MemoryStream();
            BigEndianStream bigEndianStream = new BigEndianStream(memStream);

            var testBytes = new byte[1024];

            new Random().NextBytes(testBytes);

            await bigEndianStream.WriteAsync(testBytes);

            memStream.Seek(0, SeekOrigin.Begin);

            var result = await bigEndianStream.ReadBytesAsync(1024);

            for (int i = 0; i < 1024; i++)
            {
                Assert.AreEqual(testBytes[i], result[i]);
            }
        }
    }
}
