using Noteslider.Assets.Model;
using Noteslider.Code;
using System.Windows.Controls;

namespace Noteslider.Assets.Renderer
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
