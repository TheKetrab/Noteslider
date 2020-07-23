using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noteslider.Code.Assets
{
    public class JpgAsset : ImageAsset
    {
        public JpgAsset(BitmapSource data) 
        {
            this.data = data;
        }

        public override BinaryAsset ToBinaryAsset()
        {
            byte[] bytes = Program.JpgToBytes(data);
            return new BinaryAsset(typeof(JpgAsset), bytes);
        }
    }


}
