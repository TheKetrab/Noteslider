using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Noteslider.Model
{
    public enum AssetType
    {
        TYPE_TXT, TYPE_DOC, TYPE_JPG, TYPE_PNG, TYPE_PDF
    }


    public abstract class Asset { }

    /** IMAGE */
    public abstract class ImageAsset : Asset { }
    public class JpgAsset : ImageAsset { }
    public class PngAsset : ImageAsset { }

    /** TEXT */
    public abstract class TextAsset : Asset { }
    public class TxtAsset : TextAsset { }
    public class DocAsset : TextAsset { }

    /** PDF */
    public class PdfAsset : Asset { }
}
