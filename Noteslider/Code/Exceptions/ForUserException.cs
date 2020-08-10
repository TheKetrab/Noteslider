using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code.Exceptions
{
    public class ForUserException : Exception
    {
        public StackPanel Panel = new StackPanel();


        public ForUserException() { }
        public ForUserException(string msg) : base(msg)
        {
            Label label = new Label() { Content = msg };
            label.HorizontalAlignment = HorizontalAlignment.Center;
            Panel.Children.Add(label);
        }

        public void ShowGUIMessage()
        {
            Window window = new Window();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Content = Panel;
            window.Show();
        }

    }
}
