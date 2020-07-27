using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Noteslider.Code.Renderer
{

    public class TrackRenderer
    {
        private Track _track;
        private List<AssetRenderer> _renderers =
            new List<AssetRenderer>();


        private void AutoScroll()
        {
            for (int i=0; i<5; i++)
            {
                Thread.Sleep(1000);
                
                Program.Window.ScrollViewer.ScrollToVerticalOffset(Program.Window.ScrollViewer.VerticalOffset +100);
                Program.Window.ScrollViewer.UpdateLayout();

            }
        }

        public TrackRenderer(Track track)
        {
            _track = track;
            foreach (var asset in _track.Assets)
            {
                var renderer = AssetRendererFactory.Instance.Create(asset);
                _renderers.Add(renderer);
            }

            
            
        }

        
        /// <summary>
        /// Called on window repaint.
        /// </summary>
        public void Render()
        {
            foreach (var renderer in _renderers)
            {
                renderer.ScaleToWidth();
            }

        }


    }
}
