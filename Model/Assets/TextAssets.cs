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
        public TxtAsset() { }

    }
    public class DocAsset : TextAsset
    {
        public DocAsset() { }
    }

    public class StringAsset : TextAsset
    {
        public StringAsset() { }
    }
}
