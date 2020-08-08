using Noteslider.Code.Renderer;
using Noteslider.Events;
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
using Noteslider.Code.Animator;

namespace Noteslider
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Playing { get; private set; }
        private ServerManager serverManager;
        private TrackRenderer trackRenderer;
        private bool _leftPanelHidden, _rightPanelHidden;
        private bool _leftPanelMoving, _rightPanelMoving;

        public MainWindow()
        {
            InitializeComponent();

            MainWindowSubscriber mainWindowSubscriber = new MainWindowSubscriber(this);
            EventAgregator.Instance.AddSubscriber(mainWindowSubscriber);
            EventAgregator.Instance.Publish(new MWSliderValChangedEvt(1));

            serverManager = new ServerManager(this);

            StateChanged += MainWindowStateChangeRaised;
            SizeChanged += MainWindow_SizeChanged;
        }

        #region --- Menu Procedures ---
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        //          ----------- MENU PROCEDURES -----------
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //

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
        #region --- Buttons Procedures ---
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        //          ----------- BUTTONS PROCEDURES ----------
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        private void MainWindowMenuNewTrackButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new NewTrackDialog();
            dialog.Show();
        }

        private void MainWindowMenuOpenTrackButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenTrackDialog();
            dialog.Show();
        }

        private void MainWindowMenuPlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (Playing) StopPlaying();
            else StartPlaying();
        }

        private void MainWindowMenuSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO !!!
        }
        #endregion
        #region --- AutoScroll ---
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        //             ----------- AUTO SCROLL -----------
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //

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
            double half = MWSlider.Maximum / 2;
            const int basePow = 2;

            while (Playing)
            {
                if (MWSlider.Value == 0) { speed = 0; time = 5; }
                else
                {
                    speed = Math.Pow(basePow, MWSlider.Value - half);
                    if (speed <= 0.5) { speed *= 2; time = 4; }
                    else if (speed <= 1) { time = 2; }
                    else { speed /= 2; time = 1; }
                }

                await AutoScroll(speed, time);
            }
        }
        public void StopPlaying()
        {
            Playing = false;
            MainWindowMenuPlayButtonImg.Source =
                ResourceHelper.LoadBitmapFromResource("Resources/IcoPlay.png");
        }
        #endregion

        public void RepaintTrackRenderer()
        {
            trackRenderer?.Render();
        }

        public void SetTrackRenderer(TrackRenderer trackRenderer)
        {
            this.trackRenderer = trackRenderer;

        }

        private async void ButtonHideRightPanel_Click(object sender, RoutedEventArgs e)
        {
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
        }

        #region --- Slider Events ---
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        //             ----------- SLIDER EVENTS -----------
        // ----- ----- ----- ----- ----- ----- ----- ----- ----- ----- //
        private void MWSliderButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            EventAgregator.Instance.Publish(new MWSliderValChangedEvt(MWSlider.Value + 1));
        }

        private void MWSliderButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            EventAgregator.Instance.Publish(new MWSliderValChangedEvt(MWSlider.Value - 1));
        }

        private void MWSliderText_TextChanged()
        {
            if (double.TryParse(MWSliderText.Text, out double newVal))
            {
                EventAgregator.Instance.Publish(new MWSliderValChangedEvt(newVal));
            } else
            {
                EventAgregator.Instance.Publish(new MWSliderValChangedEvt(MWSlider.Value));
            }
        }

        private void MWSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            EventAgregator.Instance.Publish(new MWSliderValChangedEvt(MWSlider.Value));
        }
        #endregion

        private void MWSliderText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                MWSliderText_TextChanged();
        }

        private void MWSliderText_LostFocus(object sender, RoutedEventArgs e)
        {
            MWSliderText_TextChanged();
        }

        private async void MWLeftPanelTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MWTabItemTracks.IsSelected)
            {
                await serverManager.UpdateTrackList();
            }
        }

        private async void ButtonHideLeftPanel_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            // percentage left and right panel
            LeftPanel.Width = this.ActualWidth * 0.15;
            RightPanel.Width = this.ActualWidth * 0.15;
            // TODO if panel hidden or visible -> set margin!!!
            LeftPanel.UpdateLayout();
            RightPanel.UpdateLayout();

            // scale assets
            RepaintTrackRenderer();
        }
    }
}
