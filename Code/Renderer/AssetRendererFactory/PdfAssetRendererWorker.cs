using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public class PdfAssetRendererWorker : IAssetRendererWorker
    {
        public AssetRenderer CreateInstance(Asset asset)
        {
            return new PdfAssetRenderer(asset);
        }

        public Type GetRendererType()
        {
            return typeof(PdfAssetRenderer);
        }
    }
}
