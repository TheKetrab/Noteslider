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

namespace Noteslider
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowManager notifier;


        public MainWindow()
        {
            InitializeComponent();
            notifier = new MainWindowManager(this.MainWindowMenu);
            InitEvents();
            Register();
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
            MainWindowMenuNewTrackButton.Click += 
                (s, e) => { EventAgregator.Instance.Publish(new MainWindowMenuNewTrackEvt()); };
            MainWindowMenuOpenTrackButton.Click += 
                (s, e) => { EventAgregator.Instance.Publish(new MainWindowMenuOpenTrackEvt()); };
            MainWindowMenuPlayButton.Click += 
                (s, e) => { EventAgregator.Instance.Publish(new MainWindowMenuPlayEvt()); };
            MainWindowMenuSettingsButton.Click += 
                (s, e) => { EventAgregator.Instance.Publish(new MainWindowMenuSettingsEvt()); };
        }

    }
}
