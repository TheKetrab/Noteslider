using Noteslider.Code;
using Noteslider.Code.AssetFactoryDir;
using Noteslider.Model.Assets;
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
        public string Image { get; set; }
        public int Length { get; set; }
    }

    public class Track
    {
        public TrackInfo TrackInfo { get; set; }
        public List<string> Tags;
        public List<Asset> Data;


        public Track(string author, string name, string imgPath) 
            : this()
        {
            TrackInfo.Author = author;
            TrackInfo.Name = name;
            TrackInfo.Image = imgPath;
            TrackInfo.Length = 0; // TODO
        }



        /// <summary>
        /// Only to create track using ReadTrack
        /// </summary>
        private Track()
        {
            TrackInfo = new TrackInfo();
            Tags = new List<string>();
            Data = new List<Asset>();
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

                // TAGS
                int tagsCnt = reader.ReadInt32();
                for (int i = 0; i < tagsCnt; i++) t.Tags.Add(reader.ReadString());

                // DATA
                int dataCnt = reader.ReadInt32();
                for (int i = 0; i < dataCnt; i++)
                {
                    var basset = BinaryAsset.ReadBinaryAsset(reader);
                    var asset = AssetFactory.Instance.CreateAsset(basset);
                    t.Data.Add(asset);
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
                ti.Image = reader.ReadString();
                ti.Length = reader.ReadInt32();
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
                writer.Write(TrackInfo.Image);
                writer.Write(TrackInfo.Length);

                // TAGS
                writer.Write(Tags.Count);
                foreach (var t in Tags) writer.Write(t);

                // DATA
                writer.Write(Data.Count);
                foreach (var asset in Data)
                {
                    var basset = AssetFactory.Instance.SerializeAsset(asset);
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
