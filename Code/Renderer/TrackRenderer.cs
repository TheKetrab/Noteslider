using Noteslider.Model.Assets;
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

        public TrackRenderer(Track track)
        {
            _track = track;
        }

        
        /// <summary>
        /// Called on window repaint.
        /// </summary>
        public void Render()
        {
            // TODO
            //if (_track == null) return;
            //foreach (Asset a in _track.Data)
            //    a.Renderer.Render();
        }


    }
}
