using Noteslider.Code.Assets;
using System;

namespace Noteslider.Code.Assets
{
    /** PDF */
    public class PdfAsset : Asset
    {
        public PdfAsset() { }

        public override BinaryAsset ToBinaryAsset()
        {
            throw new NotImplementedException();
        }
    }
}
