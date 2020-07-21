using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{


    /// <summary>
    /// Interpretes binary asset using worker and creates real assets (images, text files, pdf, ...)
    /// </summary>
    public class AssetFactory
    {
        private static AssetFactory _instance;
        private static object _lock = new object();

        private Dictionary<AssetType,IAssetFactoryWorker<Asset>> _workers = 
            new Dictionary<AssetType, IAssetFactoryWorker<Asset>>();

        
        public static AssetFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AssetFactory();
                        }
                    }
                }
                return _instance;
            }
        }

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

        public void AddWorker<T>(IAssetFactoryWorker<T> worker) where T:Asset
        {
            var type = worker.GetType();
            if (_workers.ContainsKey(type))
                throw new ArgumentException();

            _workers.Add(type, worker as IAssetFactoryWorker<Asset>);
        }


    }
}
