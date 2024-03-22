using System;
using System.Collections.Generic;
using Utility;
using UnityEngine;

namespace Infrastructure
{
    public class SubscriptionsContainer : IDisposable
    {
        private EventType _type;

        private List<Action> _subscriptions;

        public SubscriptionsContainer(EventType type)
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
            if (_subscriptions.IsNullOrEmpty()) return;

            for (int i = _subscriptions.Count; i-- > 0;)
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