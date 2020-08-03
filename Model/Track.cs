using Noteslider.Code;
using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider
{
    public class TrackInfo
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int ImageLen { get; set; }
        public byte[] Image { get; set; }
        public double SliderValue { get; set; }

        // other
        public string Path { get; set; }
    }

    public class Track
    {
        public TrackInfo TrackInfo { get; set; }
        public List<string> Tags;
        public List<Asset> Assets;


        public Track(string author, string name, string imgPath) 
            : this()
        {
            TrackInfo.Author = author ?? "UNKNOWN"; // TODO language
            TrackInfo.Name = name;
            TrackInfo.SliderValue = 1.0;

            if (string.IsNullOrEmpty(imgPath))
            {
                TrackInfo.Image = null;
                TrackInfo.ImageLen = 0;
            }
            else
            {
                TrackInfo.Image = File.ReadAllBytes(imgPath);
                TrackInfo.ImageLen = TrackInfo.Image.Length;
            }

        }



        /// <summary>
        /// Only to create track using ReadTrack
        /// </summary>
        private Track()
        {
            TrackInfo = new TrackInfo();
            Tags = new List<string>();
            Assets = new List<Asset>();
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
                t.TrackInfo.ImageLen = reader.ReadInt32();
                t.TrackInfo.Image = reader.ReadBytes(t.TrackInfo.ImageLen);
                t.TrackInfo.SliderValue = reader.ReadDouble();

                // TAGS
                int tagsCnt = reader.ReadInt32();
                for (int i = 0; i < tagsCnt; i++) t.Tags.Add(reader.ReadString());

                // DATA
                int dataCnt = reader.ReadInt32();
                for (int i = 0; i < dataCnt; i++)
                {
                    var basset = BinaryAsset.ReadBinaryAsset(reader);
                    var asset = AssetConverter.ResolveBinaryAsset(basset);
                    t.Assets.Add(asset);
                }

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
                ti.ImageLen = reader.ReadInt32();
                ti.Image = reader.ReadBytes(ti.ImageLen);
                ti.SliderValue = reader.ReadDouble();
            }

            return ti;
        }


        /// <summary>
        /// Writes metadata and data to file: LIB/Name.ns
        /// </summary>
        public void WriteTrack()
        {
            var path = GetTrackPath();           
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                // TRACK INFO
                writer.Write(TrackInfo.Name);
                writer.Write(TrackInfo.Author);
                writer.Write(TrackInfo.ImageLen);
                writer.Write(TrackInfo.Image);
                writer.Write(TrackInfo.SliderValue);

                // TAGS
                writer.Write(Tags.Count);
                foreach (var t in Tags) writer.Write(t);

                // ASSETS
                writer.Write(Assets.Count);
                foreach (var asset in Assets)
                {
                    var basset = asset.ToBinaryAsset();
                    basset.WriteBinaryAsset(writer);
                }
            }

        }

        public string GetTrackPath()
        {
            return Paths.Library + "/" + TrackInfo.Name + ".ns";
        }


    }
}
