using System;
using System.Windows.Media.Imaging;

namespace Noteslider.Model.Assets
{
    /** IMAGE */
    public abstract class ImageAsset : Asset
    {
        public BitmapSource data;
    }
    public class JpgAsset : ImageAsset
    {
        public JpgAsset() { Type = AssetType.TYPE_JPG; }
    }
    public class PngAsset : ImageAsset
    {
        public PngAsset() { Type = AssetType.TYPE_PNG; }
    }

}
