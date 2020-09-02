using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider
{
    public class Paths
    {
        public static string Library = Directory.GetCurrentDirectory() + @"\Data\notes";
        public static string Temp = Directory.GetCurrentDirectory() + @"\Data\.temp";
        public static string DefaultImage = Directory.GetCurrentDirectory() + @"\Data\Default.png";
    }
}
