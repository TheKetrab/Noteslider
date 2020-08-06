﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Noteslider.Code;
using Noteslider.Code.Renderer;

namespace Noteslider
{
    /// <summary>
    /// Logika interakcji dla klasy OpenTrackDialog.xaml
    /// </summary>
    public partial class OpenTrackDialog : Window
    {
        List<TrackInfo> tracks;

        public OpenTrackDialog()
        {
            InitializeComponent();
            this.AlignWindowLocationToMainWindowCenter();

            tracks = Library.LoadLibraryInfo();
            InitListView();

        }

        private void InitListView()
        {
            OTDListView.SelectionMode = System.Windows.Controls.SelectionMode.Single;
            OTDListView.Items.Clear();
            foreach (var item in tracks)
                OTDListView.Items.Add($"{item.Name} by {item.Author}");
        }

        public void SetInfo(string name, string author, byte[] bytes)
        {
            OTDInfoTitle.Content = name;
            OTDInfoAuthor.Content = author;
            OTDInfoImage.Source = bytes != null ?
                (BitmapSource)new ImageSourceConverter().ConvertFrom(bytes) :
                ResourceHelper.LoadBitmapFromResource("Resources/Default.png");
        }

        private void LoadTrack(int i)
        {
            var track = Track.ReadTrack(tracks[i].Path);
            TrackRenderer tr = new TrackRenderer(track);
            Program.MainWindow.SetTrackRenderer(tr);
            tr.Render();

            Program.MainWindow.MWInfoTitle.Content = track.TrackInfo.Name;
            Program.MainWindow.MWInfoAuthor.Content = track.TrackInfo.Author;
            Program.MainWindow.MWInfoImage.Source =
                (track.TrackInfo.Image != null && track.TrackInfo.ImageLen > 0) ?
                (BitmapSource)new ImageSourceConverter().ConvertFrom(track.TrackInfo.Image) :
                ResourceHelper.LoadBitmapFromResource("Resources/Default.png");
        }

        #region --- Events ---
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        //               ----------- EVENTS ----------
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = sender as ListViewItem;
            int i = OTDListView.NumberOnList(item);
            TrackInfo info = tracks[i];

            SetInfo(info.Name, info.Author, info.Image);
        }

        private void ListViewItem_MouseLeave(object sender, MouseEventArgs e)
        {
            int selected = OTDListView.SelectedIndex;
            if (selected == -1) SetInfo("", "", null);
            else
            {
                TrackInfo info = tracks[selected];
                SetInfo(info.Name, info.Author, info.Image);
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int i = OTDListView.SelectedIndex;
            LoadTrack(i);
            Close();
        }

        private void OTDLoadButton_Click(object sender, RoutedEventArgs e)
        {
            int i = OTDListView.SelectedIndex;
            LoadTrack(i);
            Close();
        }

        private void OTDCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        
    }
}