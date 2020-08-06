using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider
{
    public interface ISubscriber<T>
    {
        void Handle(T Notification);
    }

    public interface IEventAggregator
    {
        void AddSubscriber<T>(ISubscriber<T> Subscriber);
        void RemoveSubscriber<T>(ISubscriber<T> Subscriber);
        void Publish<T>(T Event);
    }

    public class EventAgregator : IEventAggregator
    {
        private static EventAgregator _instance;
        private static object _lock = new object();
        Dictionary<Type, List<object>> _subscribers =
            new Dictionary<Type, List<object>>();
            

        private EventAgregator() { }
        public static EventAgregator Instance
        {
            get
            {
                if (_instance == null)
                    lock(_lock)
                        if (_instance == null) 
                            _instance = new EventAgregator();
    
                return _instance;
            }
        }

        #region IEventAggregator Members

        public void AddSubscriber<T>(ISubscriber<T> Subscriber)
        {
            if (!_subscribers.ContainsKey(typeof(T)))
                _subscribers.Add(typeof(T), new List<object>());
            _subscribers[typeof(T)].Add(Subscriber);
        }

        public void RemoveSubscriber<T>(ISubscriber<T> Subscriber)
        {
            if (_subscribers.ContainsKey(typeof(T)))
                _subscribers[typeof(T)].Remove(Subscriber);
        }
            
        public void Publish<T>(T Event)
        {
            if (_subscribers.ContainsKey(typeof(T)))
                foreach (ISubscriber<T> subscriber in
                _subscribers[typeof(T)].OfType<ISubscriber<T>>())
                    subscriber.Handle(Event);
        }

        #endregion
    }
}
