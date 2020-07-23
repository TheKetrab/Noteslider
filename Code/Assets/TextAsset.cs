using System;
using System.Text;

namespace Noteslider.Code.Assets
{
    public class TextAsset : Asset
    {
        public string data;

        public TextAsset(string data)
        {
            this.data = data;
        }


        public override BinaryAsset ToBinaryAsset()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return new BinaryAsset(typeof(TextAsset), bytes);
        }
    }

}
