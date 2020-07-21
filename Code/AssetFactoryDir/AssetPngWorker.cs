using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noteslider.Code.AssetFactoryDir
{

    public class AssetPngWorker : IAssetFactoryWorker
    {
        public AssetPngWorker()
        {

        }
        public Asset CreateAsset(BinaryAsset basset)
        {
            BitmapSource s = (BitmapSource)new ImageSourceConverter().ConvertFrom(basset.GetBytes());
            var asset = new PngAsset()
            {
                data = s
            };
            return asset;
        }

        public BinaryAsset SerializeAsset(Asset asset)
        {
            var pngAsset = asset as PngAsset;
            byte[] bytes = Program.PngToBytes(pngAsset.data);
            return new BinaryAsset(AssetType.TYPE_PNG, bytes);
        }

        AssetType IAssetFactoryWorker.GetType()
        {
            return AssetType.TYPE_PNG;
        }
    }
}
