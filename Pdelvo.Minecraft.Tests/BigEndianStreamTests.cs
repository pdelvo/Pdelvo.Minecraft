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
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            string testString = "Hello, World!";

            bigEndianStream.Write(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            string result = bigEndianStream.ReadString16 ();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);


            memStream.SetLength(0);

            bigEndianStream.Write8(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            result = bigEndianStream.ReadString8 ();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task StringTestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            string testString = "Hello, World!";

            await bigEndianStream.WriteAsync(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            string result = await bigEndianStream.ReadString16Async ();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);


            memStream.SetLength(0);

            bigEndianStream.Write8(testString);

            memStream.Seek(0, SeekOrigin.Begin);

            result = await bigEndianStream.ReadString8Async ();

            Assert.AreEqual(testString, result);

            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void ByteTest()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            byte testByte1 = byte.MinValue;
            byte testByte2 = byte.MaxValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadByte (), testByte1);
            Assert.AreEqual(bigEndianStream.ReadByte (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task ByteTestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            byte testByte1 = byte.MinValue;
            byte testByte2 = byte.MaxValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadByteAsync (), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadByteAsync (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void Int16Test()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            short testByte1 = short.MaxValue;
            short testByte2 = short.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadInt16 (), testByte1);
            Assert.AreEqual(bigEndianStream.ReadInt16 (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task Int16TestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            short testByte1 = short.MaxValue;
            short testByte2 = short.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadInt16Async (), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadInt16Async (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void Int32Test()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            int testByte1 = int.MaxValue;
            int testByte2 = int.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadInt32 (), testByte1);
            Assert.AreEqual(bigEndianStream.ReadInt32 (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task Int32TestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            int testByte1 = int.MaxValue;
            int testByte2 = int.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadInt32Async (), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadInt32Async (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void Int64Test()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            long testByte1 = long.MaxValue;
            long testByte2 = long.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadInt64 (), testByte1);
            Assert.AreEqual(bigEndianStream.ReadInt64 (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task Int64TestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            long testByte1 = long.MaxValue;
            long testByte2 = long.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadInt64Async (), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadInt64Async (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task DoubleTest()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            double testByte1 = double.MaxValue;
            double testByte2 = double.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadDoubleAsync (), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadDoubleAsync (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void SingleTest()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            float testByte1 = float.MaxValue;
            float testByte2 = float.MinValue;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadSingle (), testByte1);
            Assert.AreEqual(bigEndianStream.ReadSingle (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task SingleTestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            float testByte1 = float.MaxValue;
            float testByte2 = float.MinValue;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadSingleAsync (), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadSingleAsync (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void BooleanTest()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            bool testByte1 = true;
            bool testByte2 = false;

            bigEndianStream.Write(testByte1);
            bigEndianStream.Write(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(bigEndianStream.ReadBoolean (), testByte1);
            Assert.AreEqual(bigEndianStream.ReadBoolean (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public async Task BooleanTestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            bool testByte1 = true;
            bool testByte2 = false;

            await bigEndianStream.WriteAsync(testByte1);
            await bigEndianStream.WriteAsync(testByte2);

            memStream.Seek(0, SeekOrigin.Begin);

            Assert.AreEqual(await bigEndianStream.ReadBooleanAsync (), testByte1);
            Assert.AreEqual(await bigEndianStream.ReadBooleanAsync (), testByte2);
            Assert.AreEqual(memStream.Position, memStream.Length);
        }

        [TestMethod]
        public void BytesTest()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            var testBytes = new byte[1024];

            new Random ().NextBytes(testBytes);

            bigEndianStream.Write(testBytes);

            memStream.Seek(0, SeekOrigin.Begin);

            byte[] result = bigEndianStream.ReadBytes(1024);

            for (int i = 0; i < 1024; i++)
            {
                Assert.AreEqual(testBytes[i], result[i]);
            }
        }

        [TestMethod]
        public async Task BytesTestAsync()
        {
            var memStream = new MemoryStream ();
            var bigEndianStream = new BigEndianStream(memStream);

            var testBytes = new byte[1024];

            new Random ().NextBytes(testBytes);

            await bigEndianStream.WriteAsync(testBytes);

            memStream.Seek(0, SeekOrigin.Begin);

            byte[] result = await bigEndianStream.ReadBytesAsync(1024);

            for (int i = 0; i < 1024; i++)
            {
                Assert.AreEqual(testBytes[i], result[i]);
            }
        }
    }
}