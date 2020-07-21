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
        public int Length { get { return bytes.Length; } }
        public AssetType Type { get; }

        private BinaryAsset(AssetType type, byte[] bytes)
        {
            this.bytes = bytes;
            this.Type = type;
        }

        public void WriteBinaryAsset(BinaryWriter writer)
        {
            throw new NotImplementedException(); // TODO
        }

        public static BinaryAsset ReadBinaryAsset(BinaryReader reader)
        {
            throw new NotImplementedException(); // TODO
        }

    }
}
