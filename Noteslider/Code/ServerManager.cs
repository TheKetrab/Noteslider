using Noteslider.Code.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Controls;

namespace Noteslider.Code
{
    public class ServerManager
    {
        const string NotesliderServer = "https://noteslider-server.herokuapp.com/";
        private MainWindow _window;

        public ServerManager(MainWindow mainWindow)
        {
            _window = mainWindow;
        }

        public async Task UpdateTrackList()
        {
            _window.MWTabItemsTracksInfo.Content = "Loading...";
            _window.MWTabItemsTracksList.Items.Clear();
            string[] tracks = await GetTrackList();

            _window.MWTabItemsTracksInfo.Content = "";
            foreach(var track in tracks)
            {
                ListViewItem item = new ListViewItem();
                item.Content = track;
                _window.MWTabItemsTracksList.Items.Add(item);
            }

        }

        public async Task<string[]> GetTrackList()
        {
            try
            {
                var request = WebRequest.Create(NotesliderServer);
                request.Timeout = 10;
                var response = (HttpWebResponse)await Task.Factory
                        .FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = response.GetResponseStream();
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        string str = streamReader.ReadToEnd();
                        dynamic tracks = Json.Decode(str);
                        int n = tracks.Length;
                        var res = new string[n];
                        for (int i = 0; i < n; i++)
                            res[i] = tracks[i];

                        return res;
                    }
                }
                else
                {
                    return new string[] { };
                }
            } catch (WebException)
            {
                throw new ServerIsDownException();
            }
            
        }
    }
}
