using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{
    public class AssetTxtWorker : IAssetFactoryWorker
    {
        public AssetTxtWorker()
        {

        }
        public Asset CreateAsset(BinaryAsset basset)
        {
            var asset = new TxtAsset()
            {
                data = Encoding.UTF8.GetString(basset.GetBytes())
            };
            return asset;
        }

        public BinaryAsset SerializeAsset(Asset asset)
        {
            var txtAsset = asset as TxtAsset;
            byte[] bytes = Encoding.UTF8.GetBytes(txtAsset.data);
            return new BinaryAsset(asset.Type, bytes);
        }

        AssetType IAssetFactoryWorker.GetType()
        {
            return AssetType.TYPE_TXT;
        }
    }
}