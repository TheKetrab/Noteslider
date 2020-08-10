using System;
using System.Windows.Media.Imaging;

namespace Noteslider.Code
{
    public static class ResourceHelper
    {
        public static BitmapImage LoadBitmapFromResource(string filename)
        {
            string uri = $"pack://application:,,,/Noteslider;component/{filename}";
            return new BitmapImage(new Uri(uri, UriKind.Absolute));
                
        }
    }
}
