
using System;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code
{
    public static class Extension
    {
        public static void AlignWindowLocationToMainWindowCenter(this Window window)
        {
            Window parent = Application.Current.MainWindow;
            window.Left = parent.Left + (parent.Width - window.Width) / 2;
            window.Top = parent.Top + (parent.Height - window.Height) / 2;
        }

        public static int NumberOnList(this ListView list, ListViewItem item)
        {
            for (int i = 0; i < list.Items.Count; i++)
                if (list.ItemContainerGenerator.ContainerFromIndex(i) == item)
                    return i;

            return -1;
        }

        public static double RoundToDecPlaces(this double d, int places)
        {
            if (places < 0) places = 0;
            long tenPow = (long)Math.Pow(10, places);
            return Math.Round(d * tenPow) / tenPow;
        }

    }
}
