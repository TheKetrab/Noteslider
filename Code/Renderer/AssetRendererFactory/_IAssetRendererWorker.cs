using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Renderer
{
    public interface IAssetRendererWorker
    {
        AssetRenderer CreateInstance(Asset asset);
    }
}
