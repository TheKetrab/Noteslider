using Noteslider.Code.AssetFactoryDir;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Noteslider.Code
{

    public class Program
    {

        public static void MainFunc(StartupEventArgs e)
        {
            var af = AssetFactory.Instance;
            af.AddWorker(new AssetJpgWorker());
            af.AddWorker(new AssetPngWorker());
            af.AddWorker(new AssetPdfWorker());
            af.AddWorker(new AssetTxtWorker());
            af.AddWorker(new AssetDocWorker());

            // COMPOSITION ROOT

        }

        public static byte[] JpgToBytes(BitmapSource source)
        {
            var encoder = new JpegBitmapEncoder();
            var frame = BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
        }
        public static byte[] PngToBytes(BitmapSource source)
        {
            var encoder = new PngBitmapEncoder();
            var frame = BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
        }


    }
}
