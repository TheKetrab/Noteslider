using Noteslider.Code;
using Noteslider.Code.Renderer;
using Noteslider.Code.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Noteslider.Events
{
    public class MainWindowManager 
        : ISubscriber<MainWindowMenuNewTrackEvt>
        , ISubscriber<MainWindowMenuOpenTrackEvt>
        , ISubscriber<MainWindowMenuPlayEvt>
        , ISubscriber<MainWindowMenuSettingsEvt>
    {
        DockPanel panel;

        public MainWindowManager(DockPanel panel)
        {
            this.panel = panel;
        }

        public void Handle(MainWindowMenuNewTrackEvt Notification)
        {
            var dialog = new NewTrackDialogManager();
            dialog.Show();
        }

        public void Handle(MainWindowMenuOpenTrackEvt Notification)
        {
            var dialog = new OpenTrackDialogManager();
            dialog.Show();
        }

        public void Handle(MainWindowMenuPlayEvt Notification)
        {
            if (Notification.WantToPlay) Program.Window.StartPlaying();
            else Program.Window.StopPlaying();
        }

        public void Handle(MainWindowMenuSettingsEvt Notification)
        {
            throw new NotImplementedException();
        }

    }
}
