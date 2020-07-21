using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Model
{
    /// <summary>
    /// Represents metadata about file (in binary format)
    /// </summary>
    public class BinaryAsset
    {
        private byte[] bytes;
        public Int64 Length { get { return bytes.Length; } }
        public AssetType Type { get; }

        public BinaryAsset(AssetType type, byte[] bytes)
        {
            this.bytes = bytes;
            this.Type = type;
        }

        public byte[] GetBytes() { return bytes; }

        public void WriteBinaryAsset(BinaryWriter writer)
        {
            writer.Write((int)Type);
            writer.Write(Length);
            foreach (byte b in bytes) writer.Write(b);
        }

        public static BinaryAsset ReadBinaryAsset(BinaryReader reader)
        {
            AssetType type = (AssetType)reader.ReadInt32();
            Int64 len = reader.ReadInt64();
            byte[] bytes = new byte[len];

            for (Int64 i = 0; i < len; i++)
                bytes[i] = reader.ReadByte();

            return new BinaryAsset(type,bytes);
        }

    }
}
