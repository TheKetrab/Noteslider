using Noteslider.Code.AssetFactoryDir;
using Noteslider.Code.Renderer;
using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Noteslider.Code
{

    public class Program
    {
        public static MainWindow Window
        {
            get
            {
                return (MainWindow)Application.Current.MainWindow;
            }
        }

        public static void MainFunc(StartupEventArgs e)
        {
            /** ----- ----- -----
             * COMPOSITION ROOT *
             * ----- ----- ----- */

            // REGISTER ASSET FACTORY
            var af = AssetFactory.Instance;
            af.AddWorker(new AssetJpgWorker());
            af.AddWorker(new AssetPngWorker());
            af.AddWorker(new AssetPdfWorker());
            af.AddWorker(new AssetTxtWorker());
            af.AddWorker(new AssetDocWorker());
            af.AddWorker(new AssetStringWorker());

            // REGISTER RENDERERS
            var arf = AssetRendererFactory.Instance;
            arf.SetRendererProvider<TextAsset>(new TextAssetRendererWorker());
            arf.SetRendererProvider<ImageAsset>(new ImageAssetRendererWorker());
            arf.SetRendererProvider<PdfAsset>(new PdfAssetRendererWorker());
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

    public static class ExtensionMethods

    {
        private static Action EmptyDelegate = delegate () { };


        public static void Refresh(this UIElement uiElement)

        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
