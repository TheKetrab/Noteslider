using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noteslider.Code
{
    public static class Converter
    {
        public static BitmapSource BytesToBmpSource(byte[] bytes)
        {
            return (BitmapSource)new ImageSourceConverter()
                .ConvertFrom(bytes);
        }


        public static byte[] BitmapToBytes(BitmapSource source, BitmapEncoder encoder)
        {
            var frame = BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
        }

        public static byte[] JpgToBytes(BitmapSource source)
        {
            return BitmapToBytes(source, new JpegBitmapEncoder());
        }
        public static byte[] PngToBytes(BitmapSource source)
        {
            return BitmapToBytes(source, new PngBitmapEncoder());
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
