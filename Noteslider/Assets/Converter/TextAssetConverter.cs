using System.Text;
using Noteslider.Assets.Model;

namespace Noteslider.Assets.Converter
{
    public class TextAssetConverter
        : IAssetConvertable<TextAsset>
    {
        public TextAsset ToAsset(BinaryAsset basset)
        {
            var text = Encoding.UTF8.GetString(basset.Bytes);
            return new TextAsset(text);
        }

        public BinaryAsset ToBinaryAsset(TextAsset asset)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(asset.data);
            return new BinaryAsset(typeof(TextAsset), bytes);
        }
    }
}
