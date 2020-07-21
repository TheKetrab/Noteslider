using System;

namespace Noteslider.Model.Assets
{
    /** TEXT */
    public abstract class TextAsset : Asset
    {
        public string data;

    }
    public class TxtAsset : TextAsset
    {
        public TxtAsset() { Type = AssetType.TYPE_TXT; }

    }
    public class DocAsset : TextAsset
    {
        public DocAsset() { Type = AssetType.TYPE_DOC; }
    }
}
