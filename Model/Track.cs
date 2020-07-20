using Noteslider.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider
{
    public enum TrackType
    {
        TYPE_TEXT, TYPE_IMAGE, TYPE_PDF, TYPE_UNDEFINED
    }

    public class TrackInfo
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public int Length { get; set; }
    }

    public class Track
    {
        public TrackInfo TrackInfo { get; set; }
        public TrackType Type { get; set; }
        public List<string> Tags;
        public List<string> Data;


        public Track(TrackType type, string author, string name, string imgPath) 
            : this(type)
        {
            TrackInfo.Author = author;
            TrackInfo.Name = name;
            TrackInfo.Image = imgPath;
            TrackInfo.Length = 0; // TODO
        }

        public Track(TrackType type) : this()
        {
            Type = type;
        }

        /// <summary>
        /// Only to create track using ReadTrack
        /// </summary>
        private Track()
        {
            TrackInfo = new TrackInfo();
            Tags = new List<string>();
            Data = new List<string>();
        }

        public static TrackType GetTrackType(int num)
        {
            if (num == 0) return TrackType.TYPE_TEXT;
            if (num == 1) return TrackType.TYPE_IMAGE;
            if (num == 2) return TrackType.TYPE_PDF;
            return TrackType.TYPE_UNDEFINED;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nsPath"></param>
        /// <returns></returns>
        public static Track ReadTrack(string nsPath)
        {
            Track t = new Track();
            using (BinaryReader reader = new BinaryReader(File.OpenRead(nsPath)))
            {
                // TRACK INFO
                t.TrackInfo.Name = reader.ReadString();
                t.TrackInfo.Author = reader.ReadString();
                t.TrackInfo.Image = reader.ReadString();
                t.TrackInfo.Length = reader.ReadInt32();

                // TYPE
                byte b = reader.ReadByte();
                t.Type = (TrackType)b;

                // TAGS
                int tagsCnt = reader.ReadInt32();
                for (int i = 0; i < tagsCnt; i++) t.Tags.Add(reader.ReadString());

                // DATA
                int dataCnt = reader.ReadInt32();
                for (int i = 0; i < dataCnt; i++) t.Data.Add(reader.ReadString());
            }

            return t;
        }



        public static TrackInfo ReadTrackInfo(string nsPath)
        {
            TrackInfo ti = new TrackInfo();
            using (BinaryReader reader = new BinaryReader(File.OpenRead(nsPath)))
            {
                // TRACK INFO
                ti.Name = reader.ReadString();
                ti.Author = reader.ReadString();
                ti.Image = reader.ReadString();
                ti.Length = reader.ReadInt32();
            }

            return ti;
        }


        /// <summary>
        /// Writes resources and .ns file to directory LIB/Author/Name/
        /// </summary>
        public void WriteTrack()
        {
            var path = String.Format("{0}/{1}/{2}.ns", Paths.Library, TrackInfo.Name, TrackInfo.Name);           
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                // TRACK INFO
                writer.Write(TrackInfo.Name);
                writer.Write(TrackInfo.Author);
                writer.Write(TrackInfo.Image);
                writer.Write(TrackInfo.Length);

                // TYPE
                writer.Write((byte)Type);

                // TAGS
                writer.Write(Tags.Count);
                foreach (var t in Tags) writer.Write(t);

                // DATA
                writer.Write(Data.Count);
                foreach (var d in Data) writer.Write(d);
            }

        }

        public string GetTrackDirPath()
        {
            return Paths.Library + "/" + TrackInfo.Name;
        }


    }
}
