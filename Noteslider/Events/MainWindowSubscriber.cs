using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Events
{
    public class MainWindowSubscriber : ISubscriber<MWSliderValChangedEvt>
    {
        private MainWindow _mainWindow;
        public MainWindowSubscriber(MainWindow window)
        {
            _mainWindow = window;
        }

        public void Handle(MWSliderValChangedEvt Notification)
        {
            _mainWindow.MWSlider.Value = Notification.NewValue;
            _mainWindow.MWSliderText.Text = Notification.NewValue.ToString();

        }
    }
}
