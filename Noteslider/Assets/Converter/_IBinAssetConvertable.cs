using Noteslider.Assets;
using Noteslider.Assets.Model;
using System.Runtime.InteropServices;

namespace Noteslider.Assets.Converter
{
    public interface IBassetToAssetConvertable<out T>
        where T : Asset
    {
        T ToAsset(BinaryAsset basset);
    }

    public interface IAssetToBassetConvertable<in T>
        where T : Asset
    {
        BinaryAsset ToBinaryAsset(T asset);
    }

    public interface IAssetConvertable<T>
        : IBassetToAssetConvertable<T>
        , IAssetToBassetConvertable<T>
        where T : Asset
    {

    }

}
