using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Noteslider.Code.Controls
{
    public class NewsItem : StackPanel
    {
        public NewsItem(string title, DateTime date, string content) : base()
        {
            TextBlock titleText = new TextBlock()
            {
                Foreground = new SolidColorBrush(Colors.LightGray),
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5, 5, 5, 5),
                Text = $"{title}\n{date:yyyy-MM-dd}"
            };

            TextBlock contentText = new TextBlock()
            {
                Foreground = new SolidColorBrush(Colors.LightGray),
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5, 5, 5, 5),
                Text = content
            };


            Children.Add(titleText);
            Children.Add(contentText);
        }
    }
}
