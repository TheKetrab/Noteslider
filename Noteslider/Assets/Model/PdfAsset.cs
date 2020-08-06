
namespace Noteslider.Assets.Model
{
    /** PDF */
    public class PdfAsset : Asset
    {
        public string data; // path

        public PdfAsset(string path) { this.data = path; }

    }
}
