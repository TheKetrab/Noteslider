using Noteslider.Code.Renderer;
using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Documents;

namespace Noteslider.Code.Assets
{

    public abstract class Asset : IBinAssetConvertable
    {
        public abstract BinaryAsset ToBinaryAsset();
    }

}
