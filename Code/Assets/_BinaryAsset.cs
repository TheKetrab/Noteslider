using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Assets
{
    /// <summary>
    /// Represents metadata about file (in binary format)
    /// </summary>
    public class BinaryAsset
    {
        public byte[] Bytes { get; }
        public Type AssetType { get; private set; }
        public Int64 Length { get { return Bytes.Length; } }


        public BinaryAsset(Type assetType, byte[] bytes)
        {
            AssetType = assetType;
            Bytes = bytes;
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
