using Noteslider.Assets.Model;
using System.Windows;

namespace Noteslider.Assets.Renderer
{
    public abstract class AssetRenderer : UIElement
    {
        public const int MARGIN = 25;
        private Asset _asset;
        public AssetRenderer(Asset asset)
        {
            _asset = asset;
        }

        /// <summary>
        /// Property keeps handler to WPF MainWindow.
        /// </summary>
        public MainWindow Window
        {
            get { return (MainWindow)Application.Current.MainWindow; }
        }

        public abstract void ScaleToWidth();

    }
}
