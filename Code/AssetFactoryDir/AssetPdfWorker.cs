using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{

    public class AssetPdfWorker : IAssetFactoryWorker
    {
        public AssetPdfWorker()
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

        AssetType IAssetFactoryWorker.GetType()
        {
            return AssetType.TYPE_PDF;
        }
    }
}