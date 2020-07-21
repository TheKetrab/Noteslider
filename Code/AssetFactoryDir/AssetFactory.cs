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

        private Dictionary<AssetType, IAssetFactoryWorker> _workers =
            new Dictionary<AssetType, IAssetFactoryWorker>();

        
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

        public Asset CreateAsset(BinaryAsset basset)
        {
            var type = basset.Type;
            if (_workers.ContainsKey(type))
            {
                return _workers[type].CreateAsset(basset);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public BinaryAsset SerializeAsset(Asset a)
        {
            var type = a.Type;
            if (_workers.ContainsKey(type))
            {
                return _workers[type].SerializeAsset(a);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void AddWorker(IAssetFactoryWorker worker)
        {
            var type = worker.GetType();
            if (_workers.ContainsKey(type))
                throw new ArgumentException();

            _workers.Add(type, worker);
        }


    }
}
