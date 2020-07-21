using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{

    public class AssetPngWorker : IAssetFactoryWorker<PngAsset>
    {
        public PngAsset CreateAsset(BinaryAsset basset)
        {
            throw new NotImplementedException();
        }

        AssetType IAssetFactoryWorker<PngAsset>.GetType()
        {
            return AssetType.TYPE_PNG;
        }
    }
}
