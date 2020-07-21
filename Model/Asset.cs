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
        public AssetType Type { get; protected set; }

        public abstract void Render();

        public static AssetType MatchAssetType(string extension)
        {
            switch(extension.ToLower())
            {
                case ".txt": return AssetType.TYPE_TXT;
                case ".doc":
                case ".docx": return AssetType.TYPE_DOC;
                case ".jpg":
                case ".jpeg": return AssetType.TYPE_JPG;
                case ".png": return AssetType.TYPE_PNG;
                case ".pdf": return AssetType.TYPE_PDF;
            }

            throw new ArgumentException();
        }
    }

    /** IMAGE */
    public abstract class ImageAsset : Asset {
        public BitmapSource data;
        public override void Render() {
            // TODO
        }
    }
    public class JpgAsset : ImageAsset {
        public JpgAsset() { Type = AssetType.TYPE_JPG; }
    }
    public class PngAsset : ImageAsset {
        public PngAsset() { Type = AssetType.TYPE_PNG; }
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
        public TxtAsset() { Type = AssetType.TYPE_TXT; }

    }
    public class DocAsset : TextAsset {
        public DocAsset() { Type = AssetType.TYPE_DOC; }
    }

    /** PDF */
    public class PdfAsset : Asset
    {
        public PdfAsset() { Type = AssetType.TYPE_PDF; }

        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}
