using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{
    public class DocAssetWorker : IAssetFactoryWorker
    {
        public DocAssetWorker()
        {

        }

        public Asset CreateAsset(BinaryAsset basset)
        {
            throw new NotImplementedException();
        }

        public BinaryAsset SerializeAsset(Asset asset)
        {
            throw new NotImplementedException();
        }

        public Type GetAssetType()
        {
            return typeof(DocAsset);
        }
    }
}
