using Noteslider.Code.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code
{
    public class OpenTrackDialogManager
    {
        private OpenTrackDialog dialog;
        private List<TrackInfo> lib;

        public OpenTrackDialogManager()
        {
            dialog = new OpenTrackDialog();
            dialog.OTDListView.SelectionMode = System.Windows.Controls.SelectionMode.Single;

            lib = Library.LoadLibraryInfo();

            dialog.OTDListView.Items.Clear();
            foreach (var item in lib)
                dialog.OTDListView.Items.Add($"{item.Name} by {item.Author}");

            InitEvents();
        }


        private void InitEvents()
        {
            dialog.OTDLoadButton.Click += (s, e) => {
                int i = dialog.OTDListView.SelectedIndex;
                var track = Track.ReadTrack(lib[i].Path);
                TrackRenderer tr = new TrackRenderer(track);
                tr.Render();
                // TODO
            };
            dialog.OTDCancelButton.Click += (s, e) => { dialog.Close(); };
        }

        public void Show()
        {
            dialog.ShowDialog();
        }
    }
}
