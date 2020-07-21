using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{
    public interface IAssetFactoryWorker<T> where T : Asset
    {
        T CreateAsset(BinaryAsset basset);
        AssetType GetType();
    }

}
