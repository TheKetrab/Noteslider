using Noteslider.Code.Renderer;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Documents;

namespace Noteslider.Model.Assets
{

    public abstract class Asset {

        private static Dictionary<string, Type> _extensions =
            new Dictionary<string, Type>();

        public string GetAssetType()
        {
            return this.GetType().Name;
        }

        public static void RegisterExtension<T>(string extension) where T:Asset
        {
            if (extension.Length < 2 || extension[0] != '.')
                throw new ArgumentException("Extension should have format '.x'");

            _extensions[extension.ToLower()] = typeof(T);
        }

        public static Type GetAssetType(string extension)
        {
            if (_extensions.ContainsKey(extension))
                return _extensions[extension];
            else
                throw new ArgumentException();
        }
    }

}
