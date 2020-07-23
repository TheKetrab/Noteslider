using Noteslider.Model.Assets;
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
        Type GetRendererType();
        bool ResponsibleFor(string extension);
    }
}
