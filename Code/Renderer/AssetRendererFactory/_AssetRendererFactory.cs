using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public class AssetRendererFactory
    {
        // register concrete renderer for concrete type first
        private Dictionary<Type, IAssetRendererWorker> _renderers =
            new Dictionary<Type, IAssetRendererWorker>();

        private static AssetRendererFactory _instance;
        private static object _lock = new object();
        private AssetRendererFactory() { }
        public static AssetRendererFactory Instance
        {
            get
            {
                if (_instance == null)
                    lock (_lock)
                        if (_instance == null)
                            _instance = new AssetRendererFactory();
                return _instance;
            }
        }

        /// <summary>
        /// Selects renderer to use, when you want to render Asset of type 'TYPE'
        /// </summary>
        public void SetRendererProvider<T>(IAssetRendererWorker worker) where T:Asset
        {
            if (_renderers.ContainsKey(typeof(T)))
                _renderers[typeof(T)] = worker;
            else
                _renderers.Add(typeof(T), worker);
        }

        public AssetRenderer Create<T>(T asset) where T:Asset
        {
            var type = GetContainedType(typeof(T));
            return _renderers[type]?.CreateInstance(asset);
        }


        private Type GetContainedType(Type type)
        {
            if (type.BaseType == null) return null;
            else if (_renderers.ContainsKey(type)) return type;

            else // recursive call to base type
                return GetContainedType(type.BaseType);
        }

    }
}
