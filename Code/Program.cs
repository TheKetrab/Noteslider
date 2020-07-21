using Noteslider.Code.AssetFactoryDir;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Noteslider.Code
{

    public class Program
    {

        public static void MainFunc(StartupEventArgs e)
        {
            var af = AssetFactory.Instance;
            af.AddWorker(new AssetJpgWorker());
            af.AddWorker(new AssetPngWorker());
            af.AddWorker(new AssetPdfWorker());
            af.AddWorker(new AssetTxtWorker());
            af.AddWorker(new AssetDocWorker());

            // COMPOSITION ROOT

        }
    }
}
