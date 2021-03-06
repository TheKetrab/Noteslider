﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Noteslider.Assets;
using Noteslider.Assets.Converter;
using Noteslider.Assets.Model;
using Noteslider.Code;
using Noteslider.Code.Controls;
using Noteslider.Code.Exceptions;

namespace Noteslider
{
    /// <summary>
    /// Logika interakcji dla klasy NewTrackDialog.xaml
    /// </summary>
    public partial class NewTrackDialog : Window
    {
        private const string WebAssetString = "WebAsset: ";
        private const string WebAssetPattern = "^WebAsset: (.*)$";
        private const string ModifyTrackPattern = @"\(page (.+)\).*";


        private int page;
        private byte[] imgBytes; // bytes to save and store image
        private bool _modifyMode;
        private Track _track; // for modify mode

        public NewTrackDialog()
        {
            InitializeComponent();
            this.AlignWindowLocationToMainWindowCenter();

            InitBaseEvents();
            SetPage(0);
        }

        public NewTrackDialog(Track t) : this()
        {
            _modifyMode = true;
            _track = t;

            this.NTDAuthor.Text = t.TrackInfo.Author;
            this.NTDTitle.Text = t.TrackInfo.Name;
    
            if (t.TrackInfo.ImageLen > 0)
                this.NTDImage.Source = Converter.BytesToBmpSource(t.TrackInfo.Image);

            for (int i=0; i<_track.Assets.Count; i++)
                this.NTDFiles.Items.Add($"(page {i + 1}) {_track.Assets[i].GetType().Name}");

        }


        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        // Close
        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
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
            if (list.SelectedItem == null) return;
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

            if (page == 2) NTDNextButton.Content = _modifyMode ? "Update" : "Add";
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
                bool success = CreateTrack();
                if (success) Close();
            }
            else
            {
                NTDTabControl.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Collects info from Dialog, creates new track and adds it to library.
        /// </summary>
        public bool CreateTrack()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NTDAuthor.Text)
                 || string.IsNullOrWhiteSpace(NTDTitle.Text))
                    throw new NewTrackDialogException("Name and Author cannot be empty.");

                if (!NTDAuthor.Text.IsAlphanumeric()
                 || !NTDTitle.Text.IsAlphanumeric())
                    throw new NewTrackDialogException(
                        "Name and Author must consist of alphanumeric characters only.");

                var author = NTDAuthor.Text;
                var name = NTDTitle.Text;

                Track track = new Track(author, name, imgBytes);


                if (NTDAdvancedSlidingSpeedCheckBox.IsChecked == true)
                {
                    NTDAdvancedSlidingSpeedInput.Text = 
                        NTDAdvancedSlidingSpeedInput.Text.Replace('.', ',');
                    
                    if (double.TryParse(NTDAdvancedSlidingSpeedInput.Text, out double speed))
                    {
                        if (speed < 0 || speed >= 10)
                            throw new NewTrackDialogException("Slider speed has to be decimal number between 0 and 10.");

                        speed = speed.RoundToDecPlaces(2);
                        track.TrackInfo.SliderValue = speed;
                    } else
                    {
                        throw new NewTrackDialogException("Slider speed has to be decimal number between 0 and 10.");
                    }
                }

                // REGISTER
                RegisterAssets(track);


                // PASSWORD ?
                string password = null;
                if (NTDAdvancedPasswordCheckBox.IsChecked == true)
                {
                    password = NTDAdvancedPassword.Text;
                }


                // SAVE TO FILE
                if (NTDAdvancedWriteToCheckBox.IsChecked == true)
                {
                    string dirPath = NTDAdvancedWriteTo.Text;
                    if (Directory.Exists(dirPath))
                        track.WriteTrack($"{dirPath}/{name}.ns",password);
                    else
                    {
                        var dialog = InfoDialog.ShowYesNoDialog(string.Format(
                            "Directory {0} does not exists. Do you want to create one?", dirPath), 
                            "Yes", "No"
                        );

                        if (dialog.InfoDialogState == InfoDialogState.YesNoDialogYes)
                        {
                            Directory.CreateDirectory(dirPath);
                            track.WriteTrack($"{dirPath}/{name}.ns", password);
                        }
                        else
                        {
                            throw new NewTrackDialogException("Set path you really want...");
                        }
                    }

                }
                else
                {
                    track.WriteTrack(password);
                }

                // DONE !!!
                if (_modifyMode) InfoDialog.ShowMessageDialog("Track updated successfully.");
                else             InfoDialog.ShowMessageDialog("Track created successfully.");

                // TODO EventAgregator.Instance.Publish<> publish new track event
                return true;
            } catch (ForUserException e)
            {
                e.ShowGUIMessage();
                return false;
            }
        }


        /// <summary>
        /// Collects info about data and injects it into track's assets list.
        /// </summary>
        public void RegisterAssets(Track track)
        {
            try
            {
                var filePaths = NTDFiles.Items;
                foreach (string file in filePaths)
                {
                    // WEB ASSET
                    if (Regex.IsMatch(file,WebAssetPattern))
                    {
                        Match m = Regex.Match(file, WebAssetPattern);
                        string url = m.Groups[1].Value;

                        WebAsset webAsset = new WebAsset(url);
                        track.Assets.Add(webAsset);
                        continue;
                    }

                    // MODIFY
                    if (_modifyMode)
                    {
                        Match m = Regex.Match(file, ModifyTrackPattern);
                        if (m.Success)
                        {
                            int indexOfAsset = int.Parse(m.Groups[1].Value) - 1;
                            track.Assets.Add(_track.Assets[indexOfAsset]);
                            continue;
                        }
                    }

                    // OTHER FILES
                    FileInfo fi = new FileInfo(file);
                    Type type = AssetConverter.GetAssetTypeByExtension(fi.Extension);
                    var bytes = File.ReadAllBytes(file);

                    var basset = new BinaryAsset(type, bytes);
                    var asset = AssetConverter.ResolveBinaryAsset(basset);
                    track.Assets.Add(asset);
                }
            } catch(Exception e)
            {
                throw new ForUserException(
                    "A Problem occured during registering assets. Details: " + e.Message);
            }
        }

        private void NTDWebAssetButton_Click(object sender, RoutedEventArgs e)
        {
            string item = WebAssetString + NTDWebAssetValue.Text;
            NTDFiles.Items.Add(item);
        }

        private void NTDFilesMoveUp_Click(object sender, RoutedEventArgs e)
        {
            var list = NTDFiles;
            if (list.SelectedItem == null) return;

            int i = list.Items.IndexOf(list.SelectedItem);
            if (i == 0) return;

            // swap items
            var temp = list.Items[i];
            list.Items[i] = list.Items[i-1];
            list.Items[i - 1] = temp;

            list.SelectedIndex = i - 1;
        }

        private void NTDFilesMoveDown_Click(object sender, RoutedEventArgs e)
        {
            var list = NTDFiles;
            if (list.SelectedItem == null) return;

            int i = list.Items.IndexOf(list.SelectedItem);
            if (i == list.Items.Count - 1) return;

            var temp = list.Items[i];
            list.Items[i] = list.Items[i + 1];
            list.Items[i + 1] = temp;

            list.SelectedIndex = i + 1;

        }
    }
}
