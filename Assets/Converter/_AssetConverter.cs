using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection.Emit;
using Noteslider.Assets.Model;

namespace Noteslider.Assets.Converter
{
    public static class AssetConverter
    {
        // map: extension -> 
        private static Dictionary<string, Type> _extensions =
            new Dictionary<string, Type>();

        private static Dictionary<Type, Type> _converters =
            new Dictionary<Type, Type>();

        public static void RegisterConverter<T,U>() 
            where T : Asset
            // where U: IBinAssetConvertable<Asset>
        {
            if (_converters.ContainsKey(typeof(T)))
                _converters[typeof(T)] = typeof(U);
            else _converters.Add(typeof(T), typeof(U));
        }

        public static Asset ResolveBinaryAsset(BinaryAsset basset)
        {
            Type type = _converters[basset.AssetType];
            if (type == null) throw new Exception("Not found");

            var converter = Activator.CreateInstance(type) as IBassetToAssetConvertable<Asset>;
            return converter.ToAsset(basset);
        }

        public static BinaryAsset ConvertToBinaryAsset(Asset asset)
        {
            Type type = _converters[asset.GetType()];
            if (type == null) throw new Exception("Not found");

            // TODO c# covariance, contrvariance
            dynamic converter = Activator.CreateInstance(type);
            System.Reflection.MethodInfo toBinaryAsset = type.GetMethod("ToBinaryAsset");
            return toBinaryAsset.Invoke(converter, new object[] { asset });
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
            else // DEFAULT INTERPRETE AS TEXT ASSET
                return typeof(TextAsset);
        }
    }
}
