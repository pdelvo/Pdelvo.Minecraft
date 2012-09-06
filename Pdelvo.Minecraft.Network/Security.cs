using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Pdelvo.Minecraft.Network
{
    public static class ProtocolSecurity
    {
        static RandomNumberGenerator _secureRandomGenerator = RandomNumberGenerator.Create();

        public static RSAParameters GenerateRsaKeyPair(out RSACryptoServiceProvider provider)
        {
            provider = new RSACryptoServiceProvider(1024);
            return provider.ExportParameters(true);
        }

        internal static RSAParameters GenerateRsaPublicKey(byte[] key)
        {
            AsnKeyParser parser = new AsnKeyParser(key);
            return parser.ParseRSAPublicKey();
        }

        private static RSAParameters GenerateRsaKey(byte[] key, bool isPrivate)
        {
            AsnKeyParser parser = new AsnKeyParser(key);
            return isPrivate ? parser.ParseRSAPrivateKey() : parser.ParseRSAPublicKey();
        }
        public static byte[] GenerateAes128Key()
        {
            var bytes = new byte[16];
            _secureRandomGenerator.GetBytes(bytes);
            return bytes;
        }

        public static byte[] RsaDecrypt(byte[] data, byte[] key, bool isPrivate)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            var provider = RSA.Create();
            provider.ImportParameters(GenerateRsaKey(key, isPrivate));
            return provider.DecryptValue(data);
        }

        public static byte[] RsaDecrypt(byte[] data, RSACryptoServiceProvider provider, bool isPrivate)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            return provider.Decrypt(data,false);
        }

        public static byte[] RsaEncrypt(byte[] data, byte[] key, bool isPrivate)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            var provider = (RSACryptoServiceProvider)RSA.Create();
            provider.ImportParameters(GenerateRsaKey(key, isPrivate));
            return provider.Encrypt(data, false);
        }

        public static string ComputeHash(params byte[][] bytes)
        {
            List<byte> b = new List<byte>();
            foreach (var item in bytes)
            {
                if (item == null)
                    if (bytes == null) throw new ArgumentNullException("bytes", "Inner array is null");
                b.AddRange(item);
            }
            return ProtocolCryptography.JavaHexDigest(b.ToArray());
        }
    }
}
