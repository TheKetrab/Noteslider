using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code.Renderer
{
    public abstract class AssetRenderer : UIElement
    {
        public const int MARGIN = 10;
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
