using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Noteslider.Code
{
    public class NewTrackDialogManager
    {
        private int page;
        private NewTrackDialog dialog;


        public NewTrackDialogManager()
        {
            dialog = new NewTrackDialog();
            InitEvents();
            SetPage(0);
        }

        private void InitEvents()
        {
            dialog.NTDNextButton.Click += (s, e) => { SetPage(page + 1); };
            dialog.NTDPrevButton.Click += (s, e) => { SetPage(page - 1); };
            dialog.NTDTabControl.SelectionChanged += (s,e) =>
            {
                var x = s as TabControl;
                if (x != null)
                {
                    page = x.SelectedIndex;
                    CheckPage(page);
                }
            };
        }

        private void CheckPage(int i)
        {
            // TODO GetLanguage
            if (page == 0) dialog.NTDPrevButton.Content = "Close";
            else dialog.NTDPrevButton.Content = "Previous";

            if (page == 2) dialog.NTDNextButton.Content = "Add";
            else dialog.NTDNextButton.Content = "Next";
        }

        private void SetPage(int i)
        {
            if (i < 0)
            {
                dialog.Close();
            } else if (i >= 3)
            {
                dialog.Close();
                //EventAgregator.Instance.Publish<> publish new track event
            } else
            {
                dialog.NTDTabControl.SelectedIndex = i;
            }
        }

        public void Show()
        {
            dialog.Show();
        }
    }
}
