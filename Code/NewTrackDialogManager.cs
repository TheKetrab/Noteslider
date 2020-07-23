using Microsoft.Win32;
using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Noteslider.Code
{
    public class NewTrackDialogManager
    {
        private int page;
        private NewTrackDialog dialog;


        public NewTrackDialogManager()
        {
            dialog = new NewTrackDialog();
            InitEvents();
            SetPage(0);
        }

        private void InitEvents()
        {
            dialog.NTDNextButton.Click += (s, e) => { SetPage(page + 1); };
            dialog.NTDPrevButton.Click += (s, e) => { SetPage(page - 1); };

            dialog.NTDImageButton.Click += (s, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                // start dialog
                if (openFileDialog.ShowDialog() == true)
                {
                    string path = openFileDialog.FileName;
                    dialog.NTDImage.Text = path;
                }

            };

            dialog.NTDFilesAdd.Click += (s, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                // start dialog
                if (openFileDialog.ShowDialog() == true)
                {
                    string path = openFileDialog.FileName;
                    dialog.NTDFiles.Items.Add(path);
                }
            };

            dialog.NTDFilesRemove.Click += (s, e) =>
            {
                var l = dialog.NTDFiles;
                l.Items.RemoveAt(l.Items.IndexOf(l.SelectedItem));
            };



            dialog.NTDTabControl.SelectionChanged += (s,e) =>
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
            if (page == 0) dialog.NTDPrevButton.Content = "Close";
            else dialog.NTDPrevButton.Content = "Previous";

            if (page == 2) dialog.NTDNextButton.Content = "Add";
            else dialog.NTDNextButton.Content = "Next";
        }

        private void SetPage(int i)
        {
            if (i < 0)
            {
                dialog.Close();
            } else if (i >= 3)
            {
                CreateTrack();
                dialog.Close();
            } else
            {
                dialog.NTDTabControl.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Collects info from Dialog, creates new track and adds it to library.
        /// </summary>
        public void CreateTrack()
        {
            
            var author = dialog.NTDAuthor.Text;
            var name = dialog.NTDTitle.Text;
            var image = dialog.NTDImage.Text;

            Track track = new Track(author, name, image);

            // REGISTER
            RegisterTags(track);
            RegisterAssets(track);

            // SAVE TO FILE
            track.WriteTrack();

            //EventAgregator.Instance.Publish<> publish new track event

        }



        /// <summary>
        /// Collects info about tags from dialog and adds it to track.
        /// </summary>
        public void RegisterTags(Track track)
        {
            var tags = dialog.NTDTags.Items;
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
            var filePaths = dialog.NTDFiles.Items;
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


        public void Show()
        {
            dialog.ShowDialog();
        }

    }
}
