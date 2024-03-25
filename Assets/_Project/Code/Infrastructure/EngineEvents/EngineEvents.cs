using System;
using System.Collections.Generic;
using Utility;

namespace Infrastructure
{
    public class EngineEvents : IEngineEvents, IDisposable
    {
        private Dictionary<EventType, SubscriptionsContainer> _dictionary;
        private IEngineEventsSource _source;

        public EngineEvents(IEngineEventsSource source)
        {
            _dictionary = new Dictionary<EventType, SubscriptionsContainer>();
            _source = source;

            Populate();

            _source.invoked += HandleEvent;
        }

        public void Dispose()
        {
            _source.invoked -= HandleEvent;

            foreach (var container in _dictionary.Values)
            {
                container.Dispose();
            }

            _dictionary.Clear();
            _dictionary = null;
        }

        public void Subscribe(EventType type, Action action)
        {
            if (action == null) return;
            if (_dictionary == null) return;
            if (!_dictionary.TryGetValue(type, out var container)) return;

            container.Add(action);
        }

        public void Unsubscribe(EventType type, Action action)
        {
            if (action == null) return;
            if (_dictionary == null) return;
            if (!_dictionary.TryGetValue(type, out var container)) return;

            container.Remove(action);
        }

        public float GetDeltaTime(EventType type)
        {
            return _source.GetDeltaTime(type);
        }

        private void Populate()
        {
            var types = EnumUtility.GetValues<EventType>();
            foreach (var type in types)
            {
                _dictionary[type] = new SubscriptionsContainer(type);
            }
        }

        private void HandleEvent(EventType type)
        {
            if (_dictionary == null) return;
            if (!_dictionary.TryGetValue(type, out var container)) return;

            container?.Invoke();
        }
    }
}