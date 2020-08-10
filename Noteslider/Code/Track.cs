
using Noteslider.Assets;
using Noteslider.Assets.Converter;
using Noteslider.Assets.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

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
        public List<Asset> Assets;


        public Track(string author, string name, byte[] img) 
            : this()
        {
            TrackInfo.Author = author ?? "UNKNOWN"; // TODO language
            TrackInfo.Name = name;
            TrackInfo.SliderValue = 1.0;

            if (img == null)
            {
                TrackInfo.Image = null;
                TrackInfo.ImageLen = 0;
            }
            else
            {
                TrackInfo.Image = img;
                TrackInfo.ImageLen = TrackInfo.Image.Length;
            }

        }



        /// <summary>
        /// Only to create track using ReadTrack
        /// </summary>
        private Track()
        {
            TrackInfo = new TrackInfo();
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

                if (ti.ImageLen > 0)
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
                if (TrackInfo.Image != null) writer.Write(TrackInfo.Image);
                writer.Write(TrackInfo.SliderValue);

                // ASSETS
                writer.Write(Assets.Count);
                foreach (var asset in Assets)
                {
                    var basset = AssetConverter.ConvertToBinaryAsset(asset);
                    basset.WriteBinaryAsset(writer);
                }
            }

        }

        /// <summary>
        /// Updates value of speed in .ns file.
        /// </summary>
        public void UpdateTrackSpeed(double speed)
        {
            if (speed < 0 || speed > 10) throw new ArgumentException();

            var path = GetTrackPath();
            long offset;

            // find position of speed
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                reader.ReadString();              // name
                reader.ReadString();              // author
                int imgLen = reader.ReadInt32();  // image len
                offset = reader.BaseStream.Position + imgLen;
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Open)))
            {
                writer.Seek((int)offset, SeekOrigin.Begin);
                writer.Write(speed); // write speed
            }

        }

        public string GetTrackPath()
        {
            return Paths.Library + "/" + TrackInfo.Name + ".ns";
        }


    }
}
