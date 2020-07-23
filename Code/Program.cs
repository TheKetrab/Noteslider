
using Noteslider.Code.Renderer;
using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media;

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
            AssetRendererFactoryInit();
            AssetConverterInit();


        }

        public static void AssetRendererFactoryInit()
        {
            var arf = AssetRendererFactory.Instance;
            arf.SetRendererProvider<TextAsset>(new TextAssetRendererWorker());
            arf.SetRendererProvider<ImageAsset>(new ImageAssetRendererWorker());
            arf.SetRendererProvider<PdfAsset>(new PdfAssetRendererWorker());
        }

        public static void AssetConverterInit()
        {
            AssetConverter.RegisterExtension<TextAsset>(".txt");
            AssetConverter.RegisterExtension<DocAsset>(".doc");
            AssetConverter.RegisterExtension<DocAsset>(".docx");
            AssetConverter.RegisterExtension<JpgAsset>(".jpg");
            AssetConverter.RegisterExtension<PngAsset>(".png");
            AssetConverter.RegisterExtension<PdfAsset>(".pdf");

            AssetConverter.RegisterConversionTo<TextAsset>((basset) =>
            {
                var text = Encoding.UTF8.GetString(basset.Bytes);
                return new TextAsset(text);
            });

            AssetConverter.RegisterConversionTo<PngAsset>((basset) =>
            {
                var data = (BitmapSource)new ImageSourceConverter()
                    .ConvertFrom(basset.Bytes);
                return new PngAsset(data);
            });

            AssetConverter.RegisterConversionTo<JpgAsset>((basset) =>
            {
                var data = (BitmapSource)new ImageSourceConverter()
                    .ConvertFrom(basset.Bytes);
                return new JpgAsset(data);
            });


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
