using Noteslider.Code.Controls;
using Noteslider.Code.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code
{
    public class ServerManager
    {
        const string NotesliderServer = "https://noteslider-server.herokuapp.com/";
        const int NotesliderTimeout = 10;

        private MainWindow _window;

        public ServerManager(MainWindow mainWindow)
        {
            _window = mainWindow;
        }

        public async Task UpdateTrackList()
        {
            _window.MWTabItemsTracksInfo.Content = "Loading...";
            string[] tracks = await GetTrackList();

            _window.MWTabItemsTracksList.Items.Clear();
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
                request.Timeout = NotesliderTimeout;
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

        public async Task DownloadTrack(string name)
        {
            try
            {
                var request = WebRequest.Create(NotesliderServer + name);
                request.Timeout = NotesliderTimeout;
                
                var response = (HttpWebResponse)await Task.Factory
                        .FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = response.GetResponseStream();
                    MemoryStream memoryStream = new MemoryStream();
                    await responseStream.CopyToAsync(memoryStream);

                    string path = $"{Paths.Library}/{name}.ns";
                    FileStream file = File.Create(path);
                    byte[] bytes = memoryStream.ToArray();
                    file.Write(bytes, 0, bytes.Length);
                    file.Close();

                    InfoDialog.ShowMessageDialog("Downloading complete.");
                }
                else
                {
                    throw new ForUserException("Error with downloading track.");
                }
            }
            catch (WebException)
            {
                throw new ServerIsDownException();
            }
            catch (ForUserException e)
            {
                e.ShowGUIMessage();
            }
        }
    }
}
