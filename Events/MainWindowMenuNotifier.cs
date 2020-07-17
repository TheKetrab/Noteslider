using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Noteslider.Events
{
    public class MainWindowMenuNotifier 
        : ISubscriber<MainWindowMenuNewTrackEvt>
        , ISubscriber<MainWindowMenuOpenTrackEvt>
        , ISubscriber<MainWindowMenuPlayEvt>
        , ISubscriber<MainWindowMenuSettingsEvt>
    {
        DockPanel panel;
        public MainWindowMenuNotifier(DockPanel panel)
        {
            this.panel = panel;
        }

        public void Handle(MainWindowMenuNewTrackEvt Notification)
        {
            throw new NotImplementedException();
        }

        public void Handle(MainWindowMenuOpenTrackEvt Notification)
        {
            throw new NotImplementedException();
        }

        public void Handle(MainWindowMenuPlayEvt Notification)
        {
            throw new NotImplementedException();
        }

        public void Handle(MainWindowMenuSettingsEvt Notification)
        {
            throw new NotImplementedException();
        }
    }
}
