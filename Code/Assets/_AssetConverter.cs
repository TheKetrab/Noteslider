using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Assets
{
    public static class AssetConverter
    {
        // map: extension -> 
        private static Dictionary<string, Type> _extensions =
            new Dictionary<string, Type>();

        private static Dictionary<Type, Func<BinaryAsset,Asset>> _converters =
            new Dictionary<Type, Func<BinaryAsset,Asset>>();

        public static void RegisterConversionTo<T>(Func<BinaryAsset,Asset> func) where T : Asset
        {
            if (_converters.ContainsKey(typeof(T)))
                _converters[typeof(T)] = func;
            else _converters.Add(typeof(T), func);
        }

        public static Asset ResolveBinaryAsset(BinaryAsset basset)
        {
            var f = _converters[basset.AssetType];
            return f.DynamicInvoke(basset) as Asset;
        }


        public static void RegisterExtension<T>(string extension) where T : Asset
        {
            if (extension.Length < 2 || extension[0] != '.')
                throw new ArgumentException("Extension should have format '.x'");

            var ext = extension.ToLower();
            if (_extensions.ContainsKey(ext))
                _extensions[extension.ToLower()] = typeof(T);
            else _extensions.Add(ext, typeof(T));
        }

        public static Type GetAssetTypeByExtension(string extension)
        {
            if (_extensions.ContainsKey(extension))
                return _extensions[extension];
            else
                throw new ArgumentException();
        }
    }
}
