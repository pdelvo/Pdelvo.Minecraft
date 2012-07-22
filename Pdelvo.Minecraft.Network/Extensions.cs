using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Network
{
    public static class Extensions
    {
        public static IPEndPoint ParseEndPoint(string str)
        {
            IPEndPoint ep;
            if (TryParseEndPoint(str, out ep))
                return ep;
            throw new FormatException();
        }

        public static bool TryParseEndPoint(string str, out IPEndPoint value)
        {
            value = null;
            if (string.IsNullOrEmpty(str))
            {
                value = default(IPEndPoint);
                return false;
            }
            string[] ep = str.Split(':');
            if (ep.Length != 2) return false;
            IPAddress adr;
            if (!IPAddress.TryParse(ep[0], out adr))
                adr =
                    (from x in Dns.GetHostEntry(ep[0]).AddressList
                     where x.AddressFamily == AddressFamily.InterNetwork
                     select x).FirstOrDefault();
            if (adr == null)
                return false;
            int port;
            if (!int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
                return false;

            value = new IPEndPoint(adr, port);
            return true;
        }

        /// <summary>
        /// Connects the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="timeout">The timeout.</param>
        public static bool Connect(this Socket socket, string host, int port, TimeSpan timeout)
        {
            return AsyncConnect(socket, (s, a, o) => s.BeginConnect(host, port, a, o), timeout);
        }

        public static bool Connect(this Socket socket, IPEndPoint endPoint, TimeSpan timeout)
        {
            return AsyncConnect(socket, (s, a, o) => s.BeginConnect(endPoint, a, o), timeout);
        }

        /// <summary>
        /// Connects the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="addresses">The addresses.</param>
        /// <param name="port">The port.</param>
        /// <param name="timeout">The timeout.</param>
        public static bool Connect(this Socket socket, IPAddress[] addresses, int port, TimeSpan timeout)
        {
            return AsyncConnect(socket, (s, a, o) => s.BeginConnect(addresses, port, a, o), timeout);
        }

        /// <summary>
        /// Asyncs the connect.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="connect">The connect.</param>
        /// <param name="timeout">The timeout.</param>
        private static bool AsyncConnect(Socket socket, Func<Socket, AsyncCallback, object, IAsyncResult> connect,
                                         TimeSpan timeout)
        {
            IAsyncResult asyncResult = connect(socket, null, null);
            if (!asyncResult.AsyncWaitHandle.WaitOne(timeout))
            {
                socket.Close();
                socket.EndConnect(asyncResult);
                return false;
            }
            socket.EndConnect(asyncResult);
            return socket.Connected;
        }

        public static async Task<int> ReadByteAsync(this Stream stream)
        {
            try
            {
                byte[] buffer = new byte[1];
                int count = await stream.ReadAsync(buffer, 0, 1);
                return count == 0 ? -1 : buffer[0];
            }
            catch (EndOfStreamException)
            {
                return -1;
            }
        }

        public static Task WriteByteAsync(this Stream stream, byte value)
        {
            return stream.WriteAsync(new[] { value }, 0, 1);
        }
    }
}