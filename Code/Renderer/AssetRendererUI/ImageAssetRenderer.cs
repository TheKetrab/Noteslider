using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noteslider.Code.Renderer
{
    public class ImageAssetRenderer : AssetRenderer
    {
        Image image;

        public ImageAssetRenderer(Asset asset) : base(asset) 
        {
            image = new Image();

            var imgAsset = asset as ImageAsset;
            image.Source = imgAsset.data;

            Window.MainWindowNotePanel.Children.Add(image);
        }

        public override void ScaleToWidth()
        {
            image.Width = Window.ScrollViewer.ActualWidth - MARGIN;
            Program.PrintDebug(image.Width.ToString());
            image.UpdateLayout();
        }


    }
}
