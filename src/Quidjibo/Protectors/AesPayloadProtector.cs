using System;
using System.Security.Cryptography;

namespace Quidjibo.Protectors
{
    public class AesPayloadProtector : IPayloadProtector
    {
        private readonly byte[] _key;

        public byte[] Protect(byte[] payload)
        {
            using (var algorithm = Aes.Create())
            using (var encryptor = algorithm.CreateEncryptor(_key, algorithm.IV))
            {
                var iv = algorithm.IV;
                var outputBuffer = new byte[payload.Length + iv.Length];

                Buffer.BlockCopy(iv, 0, outputBuffer, 0, iv.Length);
                encryptor.TransformBlock(payload, 0, payload.Length, outputBuffer, iv.Length);

                return outputBuffer;
            }
        }

        public byte[] Unprotect(byte[] payload)
        {
            var iv = new byte[16];
            Buffer.BlockCopy(payload, 0, iv, 0, iv.Length);

            var protectedData = new byte[payload.Length - iv.Length];
            Buffer.BlockCopy(payload, iv.Length, protectedData, 0, protectedData.Length);

            using (var algorithm = Aes.Create())
            using (var decryptor = algorithm.CreateDecryptor(_key, iv))
            {
                var data = new byte[32];
                decryptor.TransformBlock(protectedData, 0, protectedData.Length, data, 0);
                return data;
            }
        }

        public AesPayloadProtector(byte[] key)
        {
            _key = key;
        }
    }
}