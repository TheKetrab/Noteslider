using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public class TextAssetRendererWorker : AssetRendererWorker
    {
        public override AssetRenderer CreateInstance(Asset asset)
        {
            return new TextAssetRenderer(asset);
        }

        public override Type GetRendererType()
        {
            return typeof(TextAssetRenderer);
        }
    }
}
