using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Noteslider.Code.Renderer
{
    public class TextAssetRenderer : AssetRenderer
    {
        Label label;

        public TextAssetRenderer(Asset asset) : base(asset) 
        {
            label = new Label();
            label.Background = new SolidColorBrush(Colors.White);
            label.FontFamily = new System.Windows.Media.FontFamily("Lucida Console");//  FontFamily.;//  "Courier, Lucida Console, monospace";

              var txtAsset = asset as TextAsset;

            label.Content = txtAsset.data;
            Window.MainWindowNotePanel.Children.Add(label);

            ScaleToWidth();
        }

        private bool ScrollViewerHorizontalBarVisible()
        {
            //var y = Window.ScrollViewer.ActualWidth;
            //var t4 = Window.ScrollViewer.ViewportWidth;

            //label.ActualWidth

            Window.ScrollViewer.UpdateLayout();
            label.UpdateLayout();
            //var visibility = Window.ScrollViewer.ComputedHorizontalScrollBarVisibility;
            //return visibility == System.Windows.Visibility.Visible;

            return label.ActualWidth > Window.ScrollViewer.ViewportWidth - MARGIN;
        }

        public override void ScaleToWidth()
        {
            //label.Width = Window.ScrollViewer.ViewportWidth;
            //label.UpdateLayout();
            
            Window.ScrollViewer.UpdateLayout();
            for (var i = label.FontSize; !ScrollViewerHorizontalBarVisible(); i++)
                label.FontSize++;

            for (var i = label.FontSize; i > 1 && ScrollViewerHorizontalBarVisible(); i--)
                label.FontSize--;

            label.UpdateLayout();

        }



    }
}
