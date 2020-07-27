using Noteslider.Code.Assets;
using System;
using System.IO;

namespace Noteslider.Code.Assets
{
    /** PDF */
    public class PdfAsset : Asset
    {
        public string data; // path

        public PdfAsset(string path) { this.data = path; }

        public override BinaryAsset ToBinaryAsset()
        {
            var bytes = File.ReadAllBytes(this.data);
            File.Delete(this.data);

            return new BinaryAsset(typeof(PdfAsset),bytes);
        }
    }
}
