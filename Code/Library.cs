using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code
{
    public class Library
    {
        public Library()
        {

        }

        public static List<TrackInfo> LoadLibraryInfo()
        {
            List<TrackInfo> list = new List<TrackInfo>();
            DirectoryInfo di = new DirectoryInfo(Paths.Library);
            LoadLibraryInfoRecursive(di, list);
            return list;
        }

        private static void LoadLibraryInfoRecursive(DirectoryInfo di, List<TrackInfo> list)
        {
            var directories = di.GetDirectories();
            var files = di.GetFiles();

            foreach (var d in directories) 
                LoadLibraryInfoRecursive(d, list);

            foreach (var f in files)
                if (f.Extension.ToLower().Equals(".ns")) 
                {
                    var ti = Track.ReadTrackInfo(f.FullName);
                    ti.Path = f.FullName;
                    list.Add(ti);

                }

        }

    }
}
