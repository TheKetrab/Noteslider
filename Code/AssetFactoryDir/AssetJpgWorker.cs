using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{
    public class AssetJpgWorker : IAssetFactoryWorker<JpgAsset>
    {
        public JpgAsset CreateAsset(BinaryAsset basset)
        {
            throw new NotImplementedException();
        }

        AssetType IAssetFactoryWorker<JpgAsset>.GetType()
        {
            return AssetType.TYPE_JPG;
        }
    }


}
