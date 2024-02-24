using System;
using System.Collections.Generic;
using Game.Utility;

namespace Game
{
    public class EngineEvents : IEngineEvents, IDisposable
    {
        private readonly Dictionary<EventType, EventSubscriptionsContainer> _dictionary;
        private readonly IEngineEventsSource _source;

        public float deltaTime { get { return _source.deltaTime; } }

        public EngineEvents(IEngineEventsSource source)
        {
            _dictionary = new Dictionary<EventType, EventSubscriptionsContainer>();
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
        }

        public void Subscribe(EventType type, Action action)
        {
            if (action == null) return;
            if (!_dictionary.TryGetValue(type, out var container)) return;

            container.Add(action);
        }

        public void Unsubscribe(EventType type, Action action)
        {
            if (action == null) return;
            if (!_dictionary.TryGetValue(type, out var container)) return;

            container.Remove(action);
        }

        private void Populate()
        {
            var types = EnumUtility.GetValues<EventType>();
            foreach (var type in types)
            {
                _dictionary[type] = new EventSubscriptionsContainer(type);
            }
        }

        private void HandleEvent(EventType type)
        {
            if (!_dictionary.TryGetValue(type, out var container)) return;

            container?.Invoke();
        }
    }
}