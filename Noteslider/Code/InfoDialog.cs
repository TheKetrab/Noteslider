using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code
{
    // custom Message Box
    public static class InfoDialog
    {
        public static void ShowInfo(string info)
        {
            Window window = new Window()
            {
                MaxWidth = 300,
                Height = 100,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            StackPanel panel = new StackPanel();
            window.Content = panel;

            TextBlock text = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5, 5, 5, 5),
                Text = info            
            };

            Button button = new Button() 
            {
                Content = "OK",
                Width = 100,
                Margin = new Thickness(10,10,10,10)
            };

            button.Click += (s,e) => { window.Close(); };

            panel.Children.Add(text);
            panel.Children.Add(button);
            window.Show();
        }
    }
}
