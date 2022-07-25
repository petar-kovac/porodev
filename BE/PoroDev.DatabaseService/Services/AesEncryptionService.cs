using PoroDev.DatabaseService.Services.Contracts;
using System.Security.Cryptography;

namespace PoroDev.DatabaseService.Services
{
    public class AesEncryptionService : IEncryptionService
    {
        private static readonly byte[] secretKey = Convert.FromBase64String("dLz8jpZT5fpypiMiy6ZoYyInNyelyVKTWh0O5YV/DWE=");
        private static readonly byte[] secretIv = Convert.FromBase64String("Jcb8G24OjbJ1NwHF47GR9A==");

        public byte[] DecryptBytes(byte[] data)
        {
            byte[] decoded;

            using (Aes cipher = Aes.Create())
            {
                cipher.Padding = PaddingMode.ISO10126;
                cipher.Key = secretKey;

                ICryptoTransform encryptor = cipher.CreateDecryptor(cipher.Key, secretIv);

                decoded = encryptor.TransformFinalBlock(data, 0, data.Length);
            }

            return decoded;
        }

        public byte[] EncryptBytes(byte[] data)
        {
            byte[] encoded;

            using (Aes cipher = Aes.Create())
            {
                cipher.Padding = PaddingMode.ISO10126;
                cipher.Key = secretKey;

                ICryptoTransform encryptor = cipher.CreateEncryptor(cipher.Key, secretIv);

                encoded = encryptor.TransformFinalBlock(data, 0, data.Length);
            }

            return encoded;
        }
    }
}