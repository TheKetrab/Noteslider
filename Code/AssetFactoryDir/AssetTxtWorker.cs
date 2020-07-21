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
            throw new NotImplementedException();
        }

        AssetType IAssetFactoryWorker<TxtAsset>.GetType()
        {
            return AssetType.TYPE_TXT;
        }
    }
}