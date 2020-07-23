using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public abstract class AssetRendererWorker : IAssetRendererWorker
    {
        // Curiously recurring template pattern (CRTP) ?
        // --> each derived class has its own extension list
        private static Dictionary<Type,List<string>> _extensions = 
            new Dictionary<Type,List<string>>();

        public abstract AssetRenderer CreateInstance(Asset asset);
        public abstract Type GetRendererType();

        public bool ResponsibleFor(string extension)
        {
            return _extensions[GetType()].IndexOf(extension.ToLower()) > -1;
        }

        public static void AddExtension<T>(string extension)
        {
            if (extension.Length < 2 || !extension[0].Equals("."))
                throw new ArgumentException("Extension should have format '.x'");

            _extensions[typeof(T)].Add(extension.ToLower());
        }
    }
}
