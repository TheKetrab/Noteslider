using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public class TextAssetRendererWorker : IAssetRendererWorker
    {
        public AssetRenderer CreateInstance(Asset asset)
        {
            return new TextAssetRenderer(asset);
        }

        public Type GetRendererType()
        {
            return typeof(TextAssetRenderer);
        }
    }
}
