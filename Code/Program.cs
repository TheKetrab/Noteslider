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
            af.AddWorker(new JpgAssetWorker());
            af.AddWorker(new PngAssetWorker());
            af.AddWorker(new PdfAssetWorker());
            af.AddWorker(new TxtAssetWorker());
            af.AddWorker(new DocAssetWorker());
            af.AddWorker(new StringAssetWorker());

            // REGISTER RENDERERS
            var arf = AssetRendererFactory.Instance;
            arf.SetRendererProvider<TextAsset>(new TextAssetRendererWorker());
            arf.SetRendererProvider<ImageAsset>(new ImageAssetRendererWorker());
            arf.SetRendererProvider<PdfAsset>(new PdfAssetRendererWorker());


            Asset.RegisterExtension<TxtAsset>(".txt");
            Asset.RegisterExtension<DocAsset>(".doc");
            Asset.RegisterExtension<DocAsset>(".docx");
            Asset.RegisterExtension<JpgAsset>(".jpg");
            Asset.RegisterExtension<PngAsset>(".png");
            Asset.RegisterExtension<PdfAsset>(".pdf");

            /*
                        AssetRendererWorker.AddExtension<ImageAssetRendererWorker>(".jpg");
                        AssetRendererWorker.AddExtension<ImageAssetRendererWorker>(".png");
                        AssetRendererWorker.AddExtension<TextAssetRendererWorker>(".txt");
                        AssetRendererWorker.AddExtension<TextAssetRendererWorker>(".doc");
                        AssetRendererWorker.AddExtension<PdfAssetRendererWorker>(".pdf");
            */

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
