using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider
{
    public enum TrackType
    {
        TYPE_TEXT, TYPE_IMAGE, TYPE_PDF, TYPE_UNDEFINED
    }

    public class Track
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public TrackType Type { get; }
        public List<string> tags;
        public List<string> data;

        public Track(TrackType type)
        {
            Type = type;
        }

        public Track ReadTrack()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void WriteTrack()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
