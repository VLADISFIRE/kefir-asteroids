using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public partial class Systems
    {
        private Dictionary<int, HashSet<ISystem>> _systemsByCommands = new(8);

        private void DisposeEvents()
        {
            _systemsByCommands.Clear();

            _systemsByCommands = null;
        }

        private void SendEvent<T>(T @event)
        {
            var hash = EventIdentifier<T>.hash;

            if (_systemsByCommands.TryGetValue(hash, out var systems))
            {
                foreach (var system in systems)
                {
                    var cast = system as IEventReactionSystem<T>;

                    if(!system.isPlay) continue;
                    
                    cast?.ReactOn(@event);
                }
            }
        }

        private void OnSystemAddedByEvent<T>(T system) where T : ISystem
        {
            var hash = EventIdentifier<T>.hash;

            if (!_systemsByCommands.ContainsKey(hash))
                _systemsByCommands[hash] = new HashSet<ISystem>(8);

            _systemsByCommands[hash].Add(system);
        }

        private void OnSystemRemovedByEvent(ISystem system, int hash)
        {
            if (!_systemsByCommands.ContainsKey(hash)) return;

            _systemsByCommands[hash].Remove(system);
        }
    }
}