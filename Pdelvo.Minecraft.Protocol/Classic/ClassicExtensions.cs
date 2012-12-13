using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pdelvo.Minecraft.Network;

namespace Pdelvo.Minecraft.Protocol.Classic
{
    public static class ClassicExtensions
    {
        public static string ReadClassicString(this BigEndianStream stream)
        {
            byte[] bytes = stream.ReadBytes(64);

            return Encoding.ASCII.GetString(bytes).TrimEnd ();
        }

        public static async Task<string> ReadClassicStringAsync(this BigEndianStream stream)
        {
            byte[] bytes = await stream.ReadBytesAsync(64);

            return Encoding.ASCII.GetString(bytes).TrimEnd ();
        }

        public static void WriteClassicString(this BigEndianStream stream, string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (text.Length > 64)
                text = text.Substring(0, 64);
            byte[] bytes = Enumerable.Repeat((byte) 0x20, 64).ToArray ();
            byte[] txt = Encoding.ASCII.GetBytes(text);
            Buffer.BlockCopy(txt, 0, bytes, 0, txt.Length);
            stream.Write(bytes);
        }

        public static async Task WriteClassicStringAsync(this BigEndianStream stream, string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (text.Length > 64)
                text = text.Substring(0, 64);
            byte[] bytes = Enumerable.Repeat((byte) 0x20, 64).ToArray ();
            byte[] txt = Encoding.ASCII.GetBytes(text);
            Buffer.BlockCopy(txt, 0, bytes, 0, txt.Length);
            await stream.WriteAsync(bytes);
        }
    }
}