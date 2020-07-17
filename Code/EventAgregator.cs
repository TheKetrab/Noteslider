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

        public class EventAggregator : IEventAggregator
        {
            Dictionary<Type, List<object>> _subscribers =
                new Dictionary<Type, List<object>>();
            
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
