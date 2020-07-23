using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public class PdfAssetRendererWorker : AssetRendererWorker
    {
        public override AssetRenderer CreateInstance(Asset asset)
        {
            return new PdfAssetRenderer(asset);
        }

        public override Type GetRendererType()
        {
            return typeof(PdfAssetRenderer);
        }
    }
}
