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
            var type = ContainedType(asset.GetType());
            if (type == null) throw new KeyNotFoundException(string.Format(
                "None AssetRenderer registered for type: {0}", asset.GetType()));

            var rendererType = _renderers[type];
            AssetRenderer renderer = Activator.CreateInstance(rendererType, asset) as AssetRenderer;

            return renderer;
        }

        public static Type ContainedType(Type t)
        {
            do
            {
                if (_renderers.ContainsKey(t)) return t;
                else t = t.BaseType;

            } while (t != null);

            return null;
        }
    }
}
