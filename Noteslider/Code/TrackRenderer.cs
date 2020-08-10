using Noteslider.Assets.Renderer;
using System.Collections.Generic;

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
                // create renderer for current asset
                var renderer = AssetRendererFactory.CreateRenderer(asset);
                _renderers.Add(renderer);
            }
            
        }

        public Track GetTrack()
        {
            return _track;
        }

        
        /// <summary>
        /// Called on event SizeChanged.
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
