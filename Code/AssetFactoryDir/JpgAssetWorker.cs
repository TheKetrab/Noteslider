using Noteslider.Model.Assets;
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
    public class JpgAssetWorker : IAssetFactoryWorker
    {
        public JpgAssetWorker()
        {

        }
        public Asset CreateAsset(BinaryAsset basset)
        {
            BitmapSource s = (BitmapSource)new ImageSourceConverter().ConvertFrom(basset.GetBytes());
            var asset = new JpgAsset()
            {
                data = s
            };
            return asset;
        }

        public BinaryAsset SerializeAsset(Asset asset)
        {
            var jpgAsset = asset as JpgAsset;
            byte[] bytes = Program.JpgToBytes(jpgAsset.data);
            return new BinaryAsset(GetAssetType(), bytes);
        }

        public Type GetAssetType()
        {
            return typeof(JpgAsset);
        }
    }


}
