using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{
    public interface IAssetFactoryWorker
    {
        Asset CreateAsset(BinaryAsset basset);
        BinaryAsset SerializeAsset(Asset asset);
        AssetType GetType();
    }

}
