using Noteslider.Code.Renderer;
using Noteslider.Events;
using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using Noteslider.Code;
using Windows.UI.ViewManagement;

namespace Noteslider
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowManager notifier;
        private TrackRenderer trackRenderer;
        public bool Playing { get; private set; }


        private bool _leftPanelHidden, _rightPanelHidden;
        private bool _leftPanelMoving, _rightPanelMoving;





        #region Menu

        // Can execute
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Minimize
        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        // Maximize
        private void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        // Restore
        private void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        // Close
        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        // State change
        private void MainWindowStateChangeRaised(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MainWindowBorder.BorderThickness = new Thickness(8);
                RestoreButton.Visibility = Visibility.Visible;
                MaximizeButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainWindowBorder.BorderThickness = new Thickness(1);
                RestoreButton.Visibility = Visibility.Collapsed;
                MaximizeButton.Visibility = Visibility.Visible;
            }
        }

        #endregion







        public void RepaintTrackRenderer()
        {
            trackRenderer?.Render();
        }

        public void StopPlaying()
        {
            Playing = false;
            MainWindowMenuPlayButtonImg.Source =
                ResourceHelper.LoadBitmapFromResource("Resources/IcoPlay.png");
        }

        public void SetTrackRenderer(TrackRenderer trackRenderer)
        {
            this.trackRenderer = trackRenderer;

        }

        public MainWindow()
        {
            InitializeComponent();
            notifier = new MainWindowManager(this.MainWindowMenu);
            InitEvents();
            Register();

            StateChanged += MainWindowStateChangeRaised;

        }


        public async Task AutoScroll(double offset, int millisecondsDelay)
        {
            await Task.Delay(millisecondsDelay);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset + offset);
            ScrollViewer.UpdateLayout();
        }

        public async void StartPlaying()
        {
            Playing = true;
            MainWindowMenuPlayButtonImg.Source = 
                ResourceHelper.LoadBitmapFromResource("Resources/IcoPause.png");

            double speed;
            int time;
            double half = SpeedSlider.Maximum / 2;
            const int basePow = 2;

            while(Playing)
            {
                if (SpeedSlider.Value == 0) { speed = 0; time = 5; }
                else
                {
                    speed = Math.Pow(basePow, SpeedSlider.Value - half);
                    if (speed <= 0.5) { speed *= 2; time = 4; }
                    else if (speed <= 1) { time = 2; }
                    else { speed /= 2; time = 1; }
                }
                
                await AutoScroll(speed, time);
            }
        }

        public void Register()
        {
            EventAgregator.Instance.AddSubscriber<MainWindowMenuNewTrackEvt>(notifier);
            EventAgregator.Instance.AddSubscriber<MainWindowMenuOpenTrackEvt>(notifier);
            EventAgregator.Instance.AddSubscriber<MainWindowMenuPlayEvt>(notifier);
            EventAgregator.Instance.AddSubscriber<MainWindowMenuSettingsEvt>(notifier);
        }

 

        public void InitEvents()
        {
            SizeChanged += (s,e) =>
            {
                // ***** ***** *****
                // RENDER ON RESIZE
                trackRenderer?.Render();
                // ***** ***** *****


                // percentage left and right panel
                
                LeftPanel.Width = this.ActualWidth * 0.15;
                RightPanel.Width = this.ActualWidth * 0.15;
                //LeftPanel.UpdateLayout();
                //RightPanel.UpdateLayout();
                // TODO if panel hidden or visible -> set margin!!!


                //LeftPanel.Width = Math.Max(LeftPanel.Width, tabItemWidth * 3);
            };


            MainWindowMenuNewTrackButton.Click += 
                (s, e) => { EventAgregator.Instance.Publish(new MainWindowMenuNewTrackEvt()); };
            MainWindowMenuOpenTrackButton.Click += 
                (s, e) => { EventAgregator.Instance.Publish(new MainWindowMenuOpenTrackEvt()); };
            MainWindowMenuPlayButton.Click += 
                (s, e) => {
                    if (Playing) EventAgregator.Instance.Publish(new MainWindowMenuPlayEvt(false)); 
                    else EventAgregator.Instance.Publish(new MainWindowMenuPlayEvt(true));
                };
            MainWindowMenuSettingsButton.Click += 
                (s, e) => { EventAgregator.Instance.Publish(new MainWindowMenuSettingsEvt()); };


            ButtonHideLeftPanel.Click += async (s,e) => {

                // exit if
                if (_leftPanelMoving) return;

                // func
                if (_leftPanelHidden)
                {
                    _leftPanelMoving = true;

                    var anim = new MarginAnimator(LeftPanel);
                    await anim.AnimateMargin(new Thickness(0, 0, 0, 0));
                    ButtonHideLeftPanel.Content = "<";

                    _leftPanelHidden = false;
                    _leftPanelMoving = false;
                } 
                
                else
                {
                    _leftPanelMoving = true;

                    double panelWidth = MWLeftPanelTabControl.ActualWidth;
                    var anim = new MarginAnimator(LeftPanel);
                    await anim.AnimateMargin(new Thickness(-panelWidth, 0, 0, 0));
                    ButtonHideLeftPanel.Content = ">";

                    _leftPanelHidden = true;
                    _leftPanelMoving = false;
                }
            };
            ButtonHideRightPanel.Click += async (s, e) => {

                // exit if
                if (_rightPanelMoving) return;

                // func
                if (_rightPanelHidden)
                {
                    _rightPanelMoving = true;

                    var anim = new MarginAnimator(RightPanel);
                    await anim.AnimateMargin(new Thickness(0, 0, 0, 0));
                    ButtonHideRightPanel.Content = ">";

                    _rightPanelHidden = false;
                    _rightPanelMoving = false;
                }

                else
                {
                    _rightPanelMoving = true;

                    double panelWidth = MWRightPanelStackPanel.ActualWidth;
                    var anim = new MarginAnimator(RightPanel);
                    await anim.AnimateMargin(new Thickness(0, 0, -panelWidth, 0));
                    ButtonHideRightPanel.Content = "<";

                    _rightPanelHidden = true;
                    _rightPanelMoving = false;
                }

            };
        }

    }
}
