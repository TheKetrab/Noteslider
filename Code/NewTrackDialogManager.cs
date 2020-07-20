using Microsoft.Win32;
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

            dialog.NTDFilesAdd.Click += (s, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                // type?
                var type = Track.GetTrackType(dialog.NTDType.SelectedIndex);
                if (type == TrackType.TYPE_TEXT) openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (type == TrackType.TYPE_IMAGE) openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (type == TrackType.TYPE_PDF) openFileDialog.Filter = "PDF Files(*.pdf)|*.pdf|All Files(*.*)|*.*";

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
            var type = Track.GetTrackType(dialog.NTDType.SelectedIndex);
            var author = dialog.NTDAuthor.Text;
            var name = dialog.NTDTitle.Text;
            var image = dialog.NTDImage.Text;

            Track track = new Track(type, author, name, image);

            // CREATE DIRECTORY
            var dir = new DirectoryInfo(Paths.Library);
            Directory.CreateDirectory(track.GetTrackDirPath());

            // REGISTER
            RegisterTags(track);
            RegisterData(track.GetTrackDirPath(),track);

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
        /// Collects info about data and copy it to specyfic directory.
        /// </summary>
        public void RegisterData(string trackDirPath, Track track)
        {
            var dataPaths = dialog.NTDFiles.Items;
            var mainPath = track.GetTrackDirPath();

            for (int i=0; i<dataPaths.Count; i++)
            {
                // COPY
                string src = dataPaths[i].ToString();
                string ext = new DirectoryInfo(src).Extension;
                string dest = String.Format("{0}/data{1}{2}", mainPath, i, ext);
                File.Copy(src, dest);

                // REGISTER
                track.Data.Add(dest);
            }
        }


        public void Show()
        {
            dialog.ShowDialog();
        }

    }
}
