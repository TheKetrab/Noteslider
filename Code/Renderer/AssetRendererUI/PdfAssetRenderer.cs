using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Noteslider.Code.Renderer
{
    public class PdfAssetRenderer : AssetRenderer
    {
        public PdfAssetRenderer(Asset asset) : base(asset) { }

        public override void ScaleToWidth()
        {
            throw new NotImplementedException();
        }


    }
}
