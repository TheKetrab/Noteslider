using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code.Controls
{
    public enum InfoDialogState
    {
        YesNoDialogYes,
        YesNoDialogNo,
        ValueDialogOK
    }

    // custom Message Box
    public class InfoDialog : Window
    {
        StackPanel panel;
        public object data;

        public InfoDialogState InfoDialogState { get; private set; }


        public InfoDialog() : base()
        {
            SizeToContent = SizeToContent.Height;
            MaxWidth = 300;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            panel = new StackPanel();
            this.Content = panel;
        }


        public static void ShowMessageDialog(string info)
        {
            InfoDialog window = new InfoDialog();
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

            window.panel.Children.Add(text);
            window.panel.Children.Add(button);
            window.ShowDialog();
        }

        /// <summary>
        /// InfoDialogState = YesNoDialogYes / YesNoDialogNo after closing the dialog
        /// </summary>
        public static InfoDialog ShowYesNoDialog(string message, string yes, string no)
        {
            InfoDialog window = new InfoDialog();
            TextBlock text = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5, 5, 5, 5),
                Text = message
            };

            StackPanel buttonsPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            Button buttonYes = new Button()
            {
                Content = yes,
                Width = 100,
                Margin = new Thickness(10, 10, 10, 10)
            };

            Button buttonNo = new Button()
            {
                Content = no,
                Width = 100,
                Margin = new Thickness(10, 10, 10, 10)
            };

            buttonYes.Click += (s, e) => { window.InfoDialogState = InfoDialogState.YesNoDialogYes; window.Close(); };
            buttonNo.Click += (s, e) => { window.InfoDialogState = InfoDialogState.YesNoDialogNo; window.Close(); };
            buttonsPanel.Children.Add(buttonYes);
            buttonsPanel.Children.Add(buttonNo);

            window.panel.Children.Add(text);
            window.panel.Children.Add(buttonsPanel);
            window.ShowDialog();

            return window;
        }

        public static InfoDialog ShowValueDialog(string message)
        {
            InfoDialog window = new InfoDialog();
            TextBlock text = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5, 5, 5, 5),
                Text = message
            };

            TextBox textBox = new TextBox()
            {
                Margin = new Thickness(10, 5, 10, 5)
            };

            Button buttonOK = new Button()
            {
                Content = "OK",
                Width = 100,
                Margin = new Thickness(10, 10, 10, 10)
            };

            buttonOK.Click += (s, e) => { 
                window.InfoDialogState = InfoDialogState.ValueDialogOK;
                window.data = textBox.Text;
                window.Close(); 
            };

            window.panel.Children.Add(text);
            window.panel.Children.Add(textBox);
            window.panel.Children.Add(buttonOK);
            window.ShowDialog();

            return window;
        }
    }

}
