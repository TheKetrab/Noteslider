using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code
{

    public interface IFactoryWorker<T> where T:Asset
    {
        T CreateAsset(BinaryAsset basset);
    }

    /// <summary>
    /// Interpretes binary asset using worker and creates real assets (images, text files, pdf, ...)
    /// </summary>
    public class AssetFactory
    {
        private Dictionary<AssetType,IFactoryWorker<Asset>> _workers;

        public Asset CreateAsset(AssetType type, BinaryAsset basset)
        {
            if (_workers.ContainsKey(type))
            {
                return _workers[type].CreateAsset(basset);
            }
            else
            {
                throw new ArgumentException();
            }
        }


    }
}
