using Noteslider.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Events
{
    public class MWSliderValChangedEvt
    {
        public double NewValue { get; }
        public MWSliderValChangedEvt(double newValue)
        {
            if (newValue > 10) NewValue = 10;
            else if (newValue < 0) NewValue = 0;
            else NewValue = newValue.RoundToDecPlaces(2);
        }
    }


    public class MainWindowMenuNewTrackEvt
    {
        
    }

    public class MainWindowMenuOpenTrackEvt
    {

    }

    public class MainWindowMenuPlayEvt
    {
        public bool WantToPlay { get; }
        public MainWindowMenuPlayEvt(bool play) { WantToPlay = play; }
    }

    public class MainWindowMenuSettingsEvt
    {

    }
}
