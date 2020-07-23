using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public class ImageAssetRendererWorker : AssetRendererWorker
    {
        public override AssetRenderer CreateInstance(Asset asset)
        {
            throw new NotImplementedException();
        }

        public override Type GetRendererType()
        {
            return typeof(ImageAssetRenderer);
        }

    }
}
