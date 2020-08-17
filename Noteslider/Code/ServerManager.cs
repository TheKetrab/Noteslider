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

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Navigation;

namespace Noteslider.Code
{
    class NewsModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public static int Compare(NewsModel x, NewsModel y)
        {
            return DateTime.Compare(x.Date, y.Date);
        }

    }


    public class ServerManager
    {
        const string NotesliderServer = "https://noteslider-server.herokuapp.com/";
        const int NotesliderTimeout = 10;

        private MainWindow _window;

        public ServerManager(MainWindow mainWindow)
        {
            _window = mainWindow;
        }

        public async Task<Stream> MakeRequest(string uri)
        {
            try
            {
                var request = WebRequest.Create(uri);
                request.Timeout = NotesliderTimeout;
                var response = (HttpWebResponse)await Task.Factory
                        .FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = response.GetResponseStream();
                    return responseStream;
                }
                else
                {
                    return null;
                }
            }
            catch (WebException)
            {
                throw new ServerIsDownException();
            }
        }


        public async Task UpdateNews()
        {
            _window.MWTabControlNewsLoading.Content = "Loading...";
            List<NewsModel> list = await GetNews();
            list.Sort(NewsModel.Compare);
            _window.MWTabControlNewsLoading.Content = "";

            foreach (var news in list)
                _window.MWTabControlNews.Children.Add(
                    new NewsItem(news.Title, news.Date, news.Content));
            
        }


        private async Task<List<NewsModel>> GetNews()
        {
            var responseStream = await MakeRequest(NotesliderServer + "news");
            using (var streamReader = new StreamReader(responseStream))
            {
                string jsonData = streamReader.ReadToEnd();
                var list = JsonSerializer.Deserialize<List<NewsModel>>(jsonData);
                return list;
            }


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
            var responseStream = await MakeRequest(NotesliderServer);
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

        public async Task DownloadTrack(string name)
        {
            var responseStream = await MakeRequest(NotesliderServer + name);
            MemoryStream memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream);

            string path = $"{Paths.Library}/{name}.ns";
            FileStream file = File.Create(path);
            byte[] bytes = memoryStream.ToArray();
            file.Write(bytes, 0, bytes.Length);
            file.Close();

            InfoDialog.ShowMessageDialog("Downloading complete.");
        }
    }
}
