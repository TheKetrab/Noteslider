using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Events
{
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
