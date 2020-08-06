
using System.Windows.Media.Imaging;

namespace Noteslider.Assets.Model
{
    /** IMAGE */
    public abstract class ImageAsset : Asset
    {
        public BitmapSource data;
        public ImageAsset(BitmapSource data) { this.data = data; }

    }

}
