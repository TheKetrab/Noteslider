using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noteslider.Code.Assets
{
    public class PngAsset : ImageAsset
    {
        public PngAsset(BitmapSource data) 
        {
            this.data = data;
        }

        

        public override BinaryAsset ToBinaryAsset()
        {
            byte[] bytes = Program.PngToBytes(data);
            return new BinaryAsset(typeof(PngAsset), bytes);
        }

    }
}
