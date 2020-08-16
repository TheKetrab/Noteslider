using Noteslider.Assets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CefSharp.Wpf;
using CefSharp;

namespace Noteslider.Assets.Renderer
{
    public class WebAssetRenderer : AssetRenderer
    {

        ChromiumWebBrowser browser;

        public WebAssetRenderer(WebAsset asset) : base(asset)
        {
            browser = new ChromiumWebBrowser(asset.data);
            browser.Width = 500;
            browser.Height = 5000;
            Window.MainWindowNotePanel.Children.Add(browser);

            browser.LoadingStateChanged += async (s, e) =>
            {
                if (!e.IsLoading) // browser.CanExecuteJavascriptInMainFrame == TRUE !
                    await FitBrowserHeightToPageContent();
            };

            
        }

        private async Task FitBrowserHeightToPageContent()
        {
            if (browser.CanExecuteJavascriptInMainFrame)
            {
                JavascriptResponse response =
                    await browser.EvaluateScriptAsync(
                        "(function() {                       " +
                        "  var _docHeight =                  " +
                        "    (document.height !== undefined) " +
                        "    ? document.height               " +
                        "    : document.body.offsetHeight;   " +
                        "                                    " +
                        "  return _docHeight;                " +
                        "})                                  " +
                        "();");

                int docHeight = (int)response.Result;
                browser.Dispatcher.Invoke(() => { browser.Height = docHeight; });
            }
        }

        public override void ScaleToWidth()
        {
            browser.Width = Window.ScrollViewer.ActualWidth - MARGIN;
            browser.UpdateLayout();

            Task.Run(async () => { await FitBrowserHeightToPageContent(); });
        }
    }
}
