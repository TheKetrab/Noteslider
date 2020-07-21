using Noteslider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.AssetFactoryDir
{

    public class AssetPdfWorker : IAssetFactoryWorker<PdfAsset>
    {
        public PdfAsset CreateAsset(BinaryAsset basset)
        {
            throw new NotImplementedException();
        }

        AssetType IAssetFactoryWorker<PdfAsset>.GetType()
        {
            return AssetType.TYPE_PDF;
        }
    }
}