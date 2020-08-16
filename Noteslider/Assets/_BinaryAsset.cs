using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;

namespace Noteslider.Assets
{
    /// <summary>
    /// Represents metadata about file (in binary format)
    /// </summary>
    public class BinaryAsset
    {
        public byte[] Bytes { get; private set; }
        public Type AssetType { get; private set; }
        public Int64 Length { get { return Bytes.Length; } }
        private string _iv = "HjMb2FH6myl0T9pNRusL";

        public BinaryAsset(Type assetType, byte[] bytes)
        {
            AssetType = assetType;
            Bytes = bytes;
        }

        public Aes CreateAes(string cipher)
        {
            
            var aes = Aes.Create();
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.FeedbackSize = 128;
            aes.Padding = PaddingMode.Zeros;

            byte[] salt = new byte[8];
            Array.Copy(aes.IV, salt, 8);
            Rfc2898DeriveBytes keyGen = new Rfc2898DeriveBytes(cipher, salt, 50);
            byte[] aesKey = keyGen.GetBytes(16);

            aes.Key = aesKey;
            aes.IV = aesKey;

            return aes;
        }

        public void EncryptBytes(string cipher)
        {
            Bytes = Noteslider.Code.Cryptography.Encrypt(Bytes, cipher);
        }

        public void DecryptBytes(string cipher)
        {
            Bytes = Noteslider.Code.Cryptography.Decrypt(Bytes, cipher);
        }


        public void WriteBinaryAsset(BinaryWriter writer)
        {
            string strType = AssetTypeToString(AssetType);
            writer.Write(strType);
            writer.Write(Length);
            foreach (byte b in Bytes) writer.Write(b);
        }

        public static BinaryAsset ReadBinaryAsset(BinaryReader reader)
        {
            string strType = reader.ReadString();
            Type type = ResolveAssetType(strType);
            Int64 len = reader.ReadInt64();
            byte[] bytes = new byte[len];

            for (Int64 i = 0; i < len; i++)
                bytes[i] = reader.ReadByte();

            return new BinaryAsset(type,bytes);
        }

        public static string AssetTypeToString(Type type)
        {
            return type.FullName;
        }

        public static Type ResolveAssetType(string strType)
        {
            return Type.GetType(strType);
        }


    }
}
