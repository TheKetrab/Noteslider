using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{
    public class AssetDocWorker : IAssetFactoryWorker<DocAsset>
    {
        public DocAsset CreateAsset(BinaryAsset basset)
        {
            throw new NotImplementedException();
        }

        AssetType IAssetFactoryWorker<DocAsset>.GetType()
        {
            return AssetType.TYPE_DOC;
        }
    }
}
