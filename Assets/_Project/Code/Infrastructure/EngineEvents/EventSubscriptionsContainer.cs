using System;
using System.Collections.Generic;

namespace Game
{
    public class EventSubscriptionsContainer : IDisposable
    {
        private EventType _type;

        private List<Action> _subscriptions;

        public EventSubscriptionsContainer(EventType type)
        {
            _type = type;

            _subscriptions = new List<Action>(32);
        }

        public void Dispose()
        {
            _subscriptions.Clear();
            _subscriptions = null;
        }

        public void Add(Action action)
        {
            _subscriptions.Add(action);
        }

        public void Remove(Action action)
        {
            _subscriptions.Remove(action);
        }

        public void Invoke()
        {
            for (int i = 0; i < _subscriptions.Count; i++)
            {
                if (_subscriptions[i] == null)
                {
                    _subscriptions.RemoveAt(i);
                    continue;
                }

                _subscriptions[i]?.Invoke();
            }
        }
    }
}