using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Exceptions
{
    public class AssetConverterException : Exception
    {
        public AssetConverterException(string msg) : base(msg) { }
    }
}
