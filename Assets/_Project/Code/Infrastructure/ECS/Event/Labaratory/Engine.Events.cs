using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public partial class Engine
    {
        private const int EVENT_HANDLERS_CAPACITY = 8;
        
        private Dictionary<int, HashSet<ISystem>> _handlers = new(EVENT_HANDLERS_CAPACITY);

        private void DisposeEvents()
        {
            _handlers.Clear();

            _handlers = null;
        }

        private void SendEvent<T>(T data) where T : struct
        {
            var hash = EventIdentifier<T>.hash;

            if (_handlers.TryGetValue(hash, out var handlers))
            {
                foreach (var system in handlers)
                {
                    var cast = system as IEventHandlerSystem<T>;

                    if (!IsPlay(system)) continue;

                    cast?.OnReact(data);
                }
            }
        }

        private void OnSystemAddedByEvent<T>(T system) where T : ISystem
        {
            if (system is IEventHandlerSystem d)
            {
                var eventDataType = d.GetType().GenericTypeArguments[0];
                var hash =  EventIdentifier.Generate(eventDataType);;
                
                if (!_handlers.ContainsKey(hash))
                    _handlers[hash] = new HashSet<ISystem>(EVENT_HANDLERS_CAPACITY);

                _handlers[hash].Add(system);
            }
        }

        private void OnSystemRemovedByEvent(ISystem system, int hash)
        {
            if (!_handlers.ContainsKey(hash)) return;

            _handlers[hash].Remove(system);
        }
    }
}