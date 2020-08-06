
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

    }
}
