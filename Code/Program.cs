
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
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Reflection;

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

            AssetConverter.RegisterConversionTo<PdfAsset>((basset) =>
            {
                // CREATE DIR IF DOES NOT EXIST
                if (!Directory.Exists(Paths.Temp))
                    Directory.CreateDirectory(Paths.Temp);

                var data = basset.Bytes;
                string tempPath;

                do // random until new name
                {
                    var randomName = RandomString(50);
                    tempPath = $"{Paths.Temp}\\{randomName}.pdf";

                } while (File.Exists(tempPath));


                var file = File.Create(tempPath);
                file.Write(data, 0, data.Length);
                file.Close();
                return new PdfAsset(tempPath);
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

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }




    }

    public static class Extension
    {
        public static void AlignWindowLocationToMainWindowCenter(this Window window)
        {
            Window parent = Application.Current.MainWindow;
            window.Left = parent.Left + (parent.Width - window.Width) / 2;
            window.Top = parent.Top + (parent.Height - window.Height) / 2;
        }

        public static int NumberOnList(this ListView list, ListViewItem item)
        {
            for (int i = 0; i < list.Items.Count; i++)
                if (list.ItemContainerGenerator.ContainerFromIndex(i) == item)
                    return i;

            return -1;
        }

    }




}
