using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Noteslider.Code
{
    public class TrackRenderer
    {
        private Track _track;
        private ScrollViewer _area;

        public TrackRenderer(Track track, ScrollViewer area)
        {
            _track = track;
            _area = area;
        }

        public void Render()
        {
            foreach (var asset in _track.Data)
            {
                asset.Render();
            }
        }

    }
}
