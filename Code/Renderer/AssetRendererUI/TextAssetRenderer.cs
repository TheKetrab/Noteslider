using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Noteslider.Code.Renderer
{
    public class TextAssetRenderer : AssetRenderer
    {
        Label label;


        public TextAssetRenderer(Asset asset) : base(asset) 
        {
            label = new Label();
            label.Content = "BLABLABLA";
            Window.MainWindowNotePanel.Children.Add(label);
        }


        /*
        public override void Render(params object[] p)
        {
            
            Label label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;

            panel.Children.Add(label);

            label.Content = "ddddddddddddddddddddddddddddddddddddddddddddDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDddddddddddddddddddddddddddddddddddddddddddddddddd";
            scrollViewer.UpdateLayout();

            //l.Content = "X";
            while (true)
            {
                var visibility = scrollViewer.ComputedHorizontalScrollBarVisibility;
                if (visibility == System.Windows.Visibility.Visible)
                {
                    l.FontSize--;
                    scrollViewer.UpdateLayout();

                    //Program.Window.Refresh();
                }
                else
                {
                    break;
                }
            }
            
        }
    */

    }
}
