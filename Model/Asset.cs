using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Noteslider.Model
{
    public enum AssetType
    {
        TYPE_TXT, TYPE_DOC, TYPE_JPG, TYPE_PNG, TYPE_PDF
    }


    public abstract class Asset {
        public AssetType Type { get; }
        public abstract void Render();
    }

    /** IMAGE */
    public abstract class ImageAsset : Asset {
        public BitmapSource data;
        public override void Render() {
            // TODO
        }
    }
    public class JpgAsset : ImageAsset {
    }
    public class PngAsset : ImageAsset {
    }

    /** TEXT */
    public abstract class TextAsset : Asset {
        public string data;
        public override void Render()
        {
            // TODO
        }
    }
    public class TxtAsset : TextAsset {

    }
    public class DocAsset : TextAsset { }

    /** PDF */
    public class PdfAsset : Asset
    {
        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}
