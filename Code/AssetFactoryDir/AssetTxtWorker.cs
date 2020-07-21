using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{
    public class AssetTxtWorker : IAssetFactoryWorker<TxtAsset>
    {
        public TxtAsset CreateAsset(BinaryAsset basset)
        {
            var asset = new TxtAsset()
            {
                data = Encoding.UTF8.GetString(basset.GetBytes())
            };
            return asset;
        }

        public BinaryAsset SerializeAsset(TxtAsset asset)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(asset.data);
            return new BinaryAsset(asset.Type, bytes);
        }

        AssetType IAssetFactoryWorker<TxtAsset>.GetType()
        {
            return AssetType.TYPE_TXT;
        }
    }
}