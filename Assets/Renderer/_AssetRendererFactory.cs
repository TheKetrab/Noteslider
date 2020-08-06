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
            var renderer = _renderers[asset.GetType()];
            return Activator.CreateInstance(renderer,asset) as AssetRenderer;
        }
    }
}
