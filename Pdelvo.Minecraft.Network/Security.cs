using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.security;
using System.Security.Cryptography;
using javax.crypto;
using javax.crypto.spec;
using java.security.spec;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;

namespace Pdelvo.Minecraft.Network
{
    public static class ProtocolSecurity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "RSA is the name of the encryption standard")]
        public static RSAKeyPair GenerateRSAKeyPair()
        {
            KeyPairGenerator keyGen = KeyPairGenerator.getInstance("RSA");
            keyGen.initialize(1024);
            var pair = keyGen.generateKeyPair();
            return  new RSAKeyPair(pair.getPrivate().getEncoded(), pair.getPublic().getEncoded());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "RSA is the name of the encryption standard")]
        internal static PublicKey GenerateRSAPublicKey(byte[] key)
        {
            X509EncodedKeySpec localX509EncodedKeySpec = new X509EncodedKeySpec(key);
            KeyFactory localKeyFactory = KeyFactory.getInstance("RSA");
            return localKeyFactory.generatePublic(localX509EncodedKeySpec);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "RSA is the name of the encryption standard")]
        private static Key GenerateRSAKey(byte[] key, bool isPrivate)
        {
            KeyFactory localKeyFactory = KeyFactory.getInstance("RSA");
            if (isPrivate)
            {
                PKCS8EncodedKeySpec spec = new PKCS8EncodedKeySpec(key);
                return localKeyFactory.generatePrivate(spec);

            }
            else
            {
                X509EncodedKeySpec localX509EncodedKeySpec = new X509EncodedKeySpec(key);
                return localKeyFactory.generatePublic(localX509EncodedKeySpec);
            }
        }

        internal static Key GenerateRS4Key()
        {
            KeyGenerator gen = KeyGenerator.getInstance("RC4");
            gen.init(128);
            return gen.generateKey();
        }
        public static Key GenerateHC256Key()
        {
            return new SecretKeySpec(new SecureRandom().generateSeed(32), "HC-256");
        }
        internal static Key GenerateAes128Key()
        {
            return new SecretKeySpec(new SecureRandom().generateSeed(16), "AES-128");
        }
        public static Key GenerateRS4Key(byte[] key)
        {
            return new SecretKeySpec(key, "RC4");
        }
        public static Key GenerateHCKey(byte[] key)
        {
            return new SecretKeySpec(key, "HC-256");
        }
        public static Key GenerateAesKey(byte[] key)
        {
            return new SecretKeySpec(key, "AES-128");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "RSA is the name of the encryption standard")]
        public static byte[] RSADecrypt(byte[] data, byte[] key, bool isPrivate)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            var c = Cipher.getInstance("RSA");
            c.init(Cipher.DECRYPT_MODE, GenerateRSAKey(key, isPrivate));
            return c.doFinal(data);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "RSA is the name of the encryption standard")]
        public static byte[] RSAEncrypt(byte[] data, byte[] key, bool isPrivate)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (key == null)
                throw new ArgumentNullException("key");
            var c = Cipher.getInstance("RSA");
            c.init(Cipher.ENCRYPT_MODE, GenerateRSAKey(key, isPrivate));
            return c.doFinal(data);
        }

        public static byte[] ComputeHash(params byte[][] bytes)
        {
            if (bytes == null) throw new ArgumentNullException("bytes");
            MessageDigest digest = MessageDigest.getInstance("SHA-1");
            foreach (var item in bytes)
            {
                if (item == null)
                    if (bytes == null) throw new ArgumentNullException("bytes", "Inner array is null");
                digest.update(item);
            }
            return digest.digest();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "RSA is the name of the encryption standard")]
    public class RSAKeyPair
    {
        byte[] _private;
        byte[] _public;

        public RSAKeyPair(byte[] privateKey, byte[] publicKey)
        {
            _private = privateKey;
            _public = publicKey;
        }

        public byte[] GetPrivate()
        {
            return _private;
        }

        public byte[] GetPublic()
        {
            return _public;
        }
    }
}
