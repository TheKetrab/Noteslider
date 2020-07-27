using Syncfusion.PdfViewer.WPF;
using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Syncfusion.Windows.PdfViewer;

namespace Noteslider.Code.Renderer
{
    public class PdfAssetRenderer : AssetRenderer
    {
        WebBrowser browser;

        public PdfAssetRenderer(Asset asset) : base(asset) {

            var pdfAsset = asset as PdfAsset;
            browser = new WebBrowser();
            browser.Navigate(Paths.PdfViewer);

            browser.LoadCompleted += (s, e) =>
            {
                browser.InvokeScript("LoadPdf", pdfAsset.data);
                browser.InvokeScript("Refresh");
            };


            browser.Height = 600;

            Window.MainWindowNotePanel.Children.Add(browser);

        }

        public override void ScaleToWidth()
        {
            browser.Width = Window.ScrollViewer.ViewportWidth - MARGIN;
            browser.UpdateLayout();
        }


    }
}
