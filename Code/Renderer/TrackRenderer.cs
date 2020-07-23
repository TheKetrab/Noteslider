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

    public class TrackRenderer
    {
        private Track _track;
        private List<AssetRenderer> _renderers =
            new List<AssetRenderer>();

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
            //foreach (Asset a in _track.Data)
            //    a.Renderer.Render();
        }


    }
}
