using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noteslider.Code.AssetFactoryDir
{
    public class AssetJpgWorker : IAssetFactoryWorker<JpgAsset>
    {
        public JpgAsset CreateAsset(BinaryAsset basset)
        {
            BitmapSource s = (BitmapSource)new ImageSourceConverter().ConvertFrom(basset.GetBytes());
            var asset = new JpgAsset()
            {
                data = s
            };
            return asset;
        }

        public BinaryAsset SerializeAsset(JpgAsset asset)
        {
            byte[] bytes = Program.JpgToBytes(asset.data);
            return new BinaryAsset(AssetType.TYPE_JPG, bytes);
        }

        AssetType IAssetFactoryWorker<JpgAsset>.GetType()
        {
            return AssetType.TYPE_JPG;
        }
    }


}
