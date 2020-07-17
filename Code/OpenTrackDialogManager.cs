using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code
{
    public class OpenTrackDialogManager
    {
        private OpenTrackDialog dialog;
        public OpenTrackDialogManager()
        {
            dialog = new OpenTrackDialog();
            LoadLibrary();
            InitEvents();
        }

        private void LoadLibrary()
        {
            // TODO
        }

        private void InitEvents()
        {
            dialog.OTDLoadButton.Click += (s, e) => { /* TODO */ };
            dialog.OTDCancelButton.Click += (s, e) => { dialog.Close(); };
        }

        public void Show()
        {
            dialog.ShowDialog();
        }
    }
}
