using System;
using System.Collections.Generic;
using Noteslider.Assets.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;

namespace Noteslider.Assets.Renderer
{
    public static class AssetRendererFactory
    {
        private static Dictionary<Type, Type> _renderers =
            new Dictionary<Type, Type>();

        public static void RegisterRenderer<T,U>() 
            where T : Asset 
            where U : AssetRenderer
        {
            if (_renderers.ContainsKey(typeof(T)))
                _renderers[typeof(T)] = typeof(U);
            else _renderers.Add(typeof(T), typeof(U));
        }

        public static AssetRenderer CreateRenderer(Asset asset)
        {
            var type = _renderers[asset.GetType()];
            AssetRenderer renderer = Activator.CreateInstance(type, asset) as AssetRenderer;
            if (renderer == null) throw new KeyNotFoundException(string.Format(
                "None AssetRenderer registered for type: {0}",type.Name));

            return renderer;
        }
    }
}
