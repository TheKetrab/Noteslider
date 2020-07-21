using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Noteslider.Code.Render
{

    public class TrackRenderer
    {
        private Track _track;
        private ScrollViewer _area;

        private Dictionary<Type, IAssetRenderer> _renderers =
            new Dictionary<Type, IAssetRenderer>();

        private static TrackRenderer _instance;
        private static object _lock = new object();


        public static TrackRenderer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TrackRenderer();
                        }
                    }
                }
                return _instance;
            }
        }

        private TrackRenderer()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            _area = window.TrackRenderArea;
        }

        public void SetActiveTrack(Track t)
        {
            _track = t;
        }


        public void Render(Asset asset, params object[] p)
        {
            if (_renderers.ContainsKey(asset.GetType()))
            {
                var renderer = _renderers[asset.GetType()];
                renderer.Render(asset, p);
            }
            else 
            {
                throw new ArgumentException();
            }
        }

    }
}
