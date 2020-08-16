using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code
{
    public class Cryptography
    {
        #region Settings
        private static int _iterations = 10;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "GtACs4f8tuMrjiTi";
        private static string _vector = "zvAmnaVKR1CwvcJk";

        #endregion

        public static byte[] Encrypt(byte[] bytes, string password)
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);

            byte[] encrypted;
            using (Aes cipher = Aes.Create())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;
                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using MemoryStream to = new MemoryStream();
                    using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                    {
                        writer.Write(bytes, 0, bytes.Length);
                        writer.FlushFinalBlock();
                        encrypted = to.ToArray();
                    }
                }
                cipher.Clear();
            }
            return encrypted;
        }

        public static byte[] Decrypt(byte[] bytes, string password)
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using (Aes cipher = Aes.Create())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes);
                    using MemoryStream from = new MemoryStream(bytes);
                    using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                    {
                        decrypted = new byte[bytes.Length];
                        decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

                cipher.Clear();
            }
            return decrypted;
        }

    }
}
