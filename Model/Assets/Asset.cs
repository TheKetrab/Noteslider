using Noteslider.Code.Renderer;
using System;
using System.Security.Cryptography;

namespace Noteslider.Model.Assets
{
    public enum AssetType
    {
        TYPE_TXT, TYPE_DOC, TYPE_JPG, TYPE_PNG, TYPE_PDF, TYPE_UNKNOWN
    }


    public abstract class Asset {
        public AssetType Type { get; protected set; }
        public AssetRenderer Renderer;

        public static AssetType MatchAssetType(string extension)
        {
            switch(extension.ToLower())
            {
                case ".txt": return AssetType.TYPE_TXT;
                case ".doc":
                case ".docx": return AssetType.TYPE_DOC;
                case ".jpg":
                case ".jpeg": return AssetType.TYPE_JPG;
                case ".png": return AssetType.TYPE_PNG;
                case ".pdf": return AssetType.TYPE_PDF;
                default: return AssetType.TYPE_UNKNOWN;
            }

            throw new ArgumentException();
        }
    }

}
