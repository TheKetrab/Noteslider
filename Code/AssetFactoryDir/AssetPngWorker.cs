using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noteslider.Code.AssetFactoryDir
{

    public class AssetPngWorker : IAssetFactoryWorker<PngAsset>
    {
        public PngAsset CreateAsset(BinaryAsset basset)
        {
            BitmapSource s = (BitmapSource)new ImageSourceConverter().ConvertFrom(basset.GetBytes());
            var asset = new PngAsset()
            {
                data = s
            };
            return asset;
        }

        public BinaryAsset SerializeAsset(PngAsset asset)
        {
            byte[] bytes = Program.PngToBytes(asset.data);
            return new BinaryAsset(AssetType.TYPE_PNG, bytes);
        }

        AssetType IAssetFactoryWorker<PngAsset>.GetType()
        {
            return AssetType.TYPE_PNG;
        }
    }
}
