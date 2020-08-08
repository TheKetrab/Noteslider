
using System.Windows;
using Noteslider.Assets.Converter;
using Noteslider.Assets.Model;
using Noteslider.Assets.Renderer;

namespace Noteslider.Code
{

    public class Program
    {
        public static void MainFunc(StartupEventArgs e)
        {
            /** ----- ----- -----
             * COMPOSITION ROOT *
             * ----- ----- ----- */
            AssetConverterInit();
            AssetRendererInit();


        }

        public static MainWindow MainWindow
        {
            get
            {
                return (MainWindow)Application.Current.MainWindow;
            }
        }


        public static void AssetConverterInit()
        {
            // use concrete asset for concrete extension
            AssetConverter.RegisterExtension<TextAsset>(".txt");
            AssetConverter.RegisterExtension<DocAsset>(".doc");
            AssetConverter.RegisterExtension<DocAsset>(".docx");
            AssetConverter.RegisterExtension<JpgAsset>(".jpg");
            AssetConverter.RegisterExtension<PngAsset>(".png");
            AssetConverter.RegisterExtension<PdfAsset>(".pdf");

            // for concrete asset use concrete asset converter
            AssetConverter.RegisterConverter<TextAsset, TextAssetConverter>();
            AssetConverter.RegisterConverter<JpgAsset,ImageAssetConverter>();
            AssetConverter.RegisterConverter<PngAsset,ImageAssetConverter> ();
            AssetConverter.RegisterConverter<PdfAsset,PdfAssetConverter>();

        }

        public static void AssetRendererInit()
        {
            // for concrete asset use concrete asset renderer
            AssetRendererFactory.RegisterRenderer<TextAsset, TextAssetRenderer>();
            AssetRendererFactory.RegisterRenderer<ImageAsset, ImageAssetRenderer>();
            AssetRendererFactory.RegisterRenderer<PdfAsset, PdfAssetRenderer>();
        }

        public static void PrintDebug(string msg)
        {
            MainWindow.DEBUG.Content = msg;
        }

    }

    




}
