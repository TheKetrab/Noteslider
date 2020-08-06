using System;
using Noteslider.Assets.Model;

namespace Noteslider.Assets.Converter
{
    public class ImageAssetConverter 
        : IAssetConvertable<ImageAsset>

    {
        public ImageAsset ToAsset(BinaryAsset basset)
        {
            var data = Code.Converter.BytesToBmpSource(basset.Bytes);
            return (ImageAsset)Activator.CreateInstance(basset.AssetType, data);
        }

        public BinaryAsset ToBinaryAsset(ImageAsset asset)
        {
            byte[] bytes = Code.Converter.PngToBytes(asset.data);
            return new BinaryAsset(typeof(PngAsset), bytes);
        }
    }
}
