using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Noteslider.Assets;
using Noteslider.Assets.Converter;
using Noteslider.Code;

namespace Noteslider
{
    /// <summary>
    /// Logika interakcji dla klasy NewTrackDialog.xaml
    /// </summary>
    public partial class NewTrackDialog : Window
    {
        private int page;
        private byte[] imgBytes; // bytes to save and store image


        public NewTrackDialog()
        {
            InitializeComponent();
            this.AlignWindowLocationToMainWindowCenter();

            InitBaseEvents();
            SetPage(0);
        }

        #region --- Buttons Procedures ---
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        //          ----------- BUTTONS PROCEDURES ----------
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        private void NTDImageButtonBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            // start dialog
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                var bytes = File.ReadAllBytes(path);
                var img = Converter.BytesToBmpSource(bytes);
                NTDImage.Source = img;
                imgBytes = bytes;
            }
        }

        private void NTDImageButtonPaste_Click(object sender, RoutedEventArgs e)
        {
            var image = System.Windows.Clipboard.GetImage();
            if (image != null)
            {
                var bytes = Converter.JpgToBytes(image);
                NTDImage.Source = image;
                imgBytes = bytes;
            }
        }

        private void NTDFilesAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // start dialog
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                NTDFiles.Items.Add(path);
            }
        }

        private void NTDFilesRemove_Click(object sender, RoutedEventArgs e)
        {
            var list = NTDFiles;
            list.Items.RemoveAt(
                list.Items.IndexOf(
                    list.SelectedItem));
        }

        #endregion


        private void InitBaseEvents()
        {
            NTDNextButton.Click += (s, e) => { SetPage(page + 1); };
            NTDPrevButton.Click += (s, e) => { SetPage(page - 1); };
            NTDTabControl.SelectionChanged += (s, e) =>
            {
                var tabControl = s as TabControl;
                if (tabControl != null)
                {
                    page = tabControl.SelectedIndex;
                    CheckPage(page);
                }
            };

        }


        private void CheckPage(int i)
        {
            // TODO GetLanguage
            if (page == 0) NTDPrevButton.Content = "Close";
            else NTDPrevButton.Content = "Previous";

            if (page == 2) NTDNextButton.Content = "Add";
            else NTDNextButton.Content = "Next";
        }

        private void SetPage(int i)
        {
            if (i < 0)
            {
                Close();
            }
            else if (i >= 3)
            {
                CreateTrack();
                Close();
            }
            else
            {
                NTDTabControl.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Collects info from Dialog, creates new track and adds it to library.
        /// </summary>
        public void CreateTrack()
        {
            var author = NTDAuthor.Text;
            var name = NTDTitle.Text;

            Track track = new Track(author, name, imgBytes);

            // REGISTER
            RegisterTags(track);
            RegisterAssets(track);

            // SAVE TO FILE
            track.WriteTrack();

            // TODO EventAgregator.Instance.Publish<> publish new track event

        }

        /// <summary>
        /// Collects info about tags from dialog and adds it to track.
        /// </summary>
        public void RegisterTags(Track track)
        {
            var tags = NTDTags.Items;
            foreach (var tag in tags)
            {
                track.Tags.Add(tag.ToString());
            }
        }

        /// <summary>
        /// Collects info about data and injects it into track's assets list.
        /// </summary>
        public void RegisterAssets(Track track)
        {
            var filePaths = NTDFiles.Items;
            foreach (string file in filePaths)
            {
                FileInfo fi = new FileInfo(file);
                Type type = AssetConverter.GetAssetTypeByExtension(fi.Extension);
                var bytes = File.ReadAllBytes(file);

                var basset = new BinaryAsset(type, bytes);
                var asset = AssetConverter.ResolveBinaryAsset(basset);
                track.Assets.Add(asset);
            }
        }


    }
}
