using System;
using System.IO;
using Noteslider.Assets.Model;
using Noteslider.Code;

namespace Noteslider.Assets.Converter
{
    public class PdfAssetConverter
        : IAssetConvertable<PdfAsset>
    {
        public PdfAsset ToAsset(BinaryAsset basset)
        {
            // CREATE DIR IF DOES NOT EXIST
            if (!Directory.Exists(Paths.Temp))
                Directory.CreateDirectory(Paths.Temp);

            var data = basset.Bytes;
            string tempPath;

            do // random until new name
            {
                var randomName = Code.Converter.RandomString(50);
                tempPath = $"{Paths.Temp}\\{randomName}.pdf";

            } while (File.Exists(tempPath));


            var file = File.Create(tempPath);
            file.Write(data, 0, data.Length);
            file.Close();
            return new PdfAsset(tempPath);
        }

        public BinaryAsset ToBinaryAsset(PdfAsset asset)
        {
            var bytes = File.ReadAllBytes(asset.data);
            File.Delete(asset.data);

            return new BinaryAsset(typeof(PdfAsset), bytes);
        }
    }
}
