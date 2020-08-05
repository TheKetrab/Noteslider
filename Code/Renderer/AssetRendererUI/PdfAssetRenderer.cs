using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.IO;
using Windows.Storage;
using Windows.Data.Pdf;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;
using System.Windows;

namespace Noteslider.Code.Renderer
{
    public class PdfAssetRenderer : AssetRenderer
    {
        // each pdf page is converted to image
        Image[] images;

        public PdfAssetRenderer(Asset asset) : base(asset) {

            var pdfAsset = asset as PdfAsset;
            // Task.Run(async () => await Procede(pdfAsset)); TODO Window.MainWindowNotePanel.Children.Add(img); exception
            Procede(pdfAsset);
        }

        private async Task Procede(PdfAsset asset)
        {

            await InterpretePdf(asset.data);

            foreach(var img in images)
                Window.MainWindowNotePanel.Children.Add(img);

            ScaleToWidth();
        }

        private static async Task<BitmapImage> PageToBitmapAsync(PdfPage page)
        {
            BitmapImage image = new BitmapImage();
            using (var stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStream();
                image.EndInit();
            }

            return image;
        }

        private async Task InterpretePdf(string pdfPath)
        {
            // 1. Read PDF
            // 2. Convert each page to Image

            var file = await StorageFile.GetFileFromPathAsync(pdfPath);
            var pdfDoc = await PdfDocument.LoadFromFileAsync(file);

            if (pdfDoc == null) return;

            images = new Image[pdfDoc.PageCount];
            for (uint i = 0; i < pdfDoc.PageCount; i++)
            {
                using (var page = pdfDoc.GetPage(i))
                {
                    var bitmap = await PageToBitmapAsync(page);
                    var image = new Image
                    {
                        Source = bitmap,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 4, 0, 4),
                        
                    };
                    images[i] = image;
                }
            }
        }

        public override void ScaleToWidth()
        {
            // scaling at the begining will be skipped
            if (images == null) return;

            foreach(var img in images)
            {
                img.Width = Window.ScrollViewer.ActualWidth - MARGIN;

                img.UpdateLayout();
                Program.PrintDebug(string.Format("Width = {0}\nActualWidth = {1}",img.Width,img.ActualWidth));
            }
        }


    }
}
