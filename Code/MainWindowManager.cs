using Noteslider.Code;
using Noteslider.Code.Renderer;
using Noteslider.Model.Assets;
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
            var track = new Track("aut","nam","imp");
            var strAsset = new StringAsset();
            strAsset.data = "blablabla";

            track.Assets.Add(strAsset);

            var renderer = AssetRendererFactory.Instance.Create(strAsset);
        }

        public void Handle(MainWindowMenuSettingsEvt Notification)
        {
            throw new NotImplementedException();
        }

    }
}
