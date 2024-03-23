using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public partial class Systems : IDisposable
    {
        private World _world;

        private List<ISystem> _systems = new(16);

        private Dictionary<int, ISystem> _dictionary = new(16);

        private bool _initialized;

        public Systems(World world)
        {
            _world = world;

            _world.beforeRemoved += HandleBeforeEntityRemoved;
            _world.removed += HandleEntityRemoved;
        }

        public void Dispose()
        {
            _world.beforeRemoved -= HandleBeforeEntityRemoved;
            _world.removed -= HandleEntityRemoved;

            _world = null;

            foreach (var system in _systems)
            {
                system?.Dispose();
            }

            _systems.Clear();
            _systems = null;

            _dictionary.Clear();
            _dictionary = null;

            DisposeEvents();
        }

        internal void Initialize()
        {
            _initialized = true;

            foreach (var system in _systems)
            {
                system.Initialize(_world);
            }
        }

        internal T Add<T>(T system, bool autoPlay = true) where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            if (_dictionary.TryGetValue(hash, out var value))
                return (T) value;

            _dictionary.Add(hash, system);
            _systems.Add(system);

            OnSystemAddedByEvent(system);

            if (_initialized)
            {
                system.Initialize(_world);
            }

            if (autoPlay)
                system.Play();

            return system;
        }

        internal void Remove<T>() where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            if (_dictionary.TryGetValue(hash, out var system))
            {
                Remove(system, hash);
            }
        }

        internal void Update(float deltaTime)
        {
            foreach (var system in _systems)
            {
                system?.Update(deltaTime);
            }
        }

        internal void LateUpdate()
        {
            foreach (var system in _systems)
            {
                system?.LateUpdate();
            }
        }

        private void Remove(ISystem system, int hash)
        {
            _dictionary.Remove(hash);
            _systems.Remove(system);

            OnSystemRemovedByEvent(system, hash);
        }

        private void HandleBeforeEntityRemoved(Entity entity)
        {
            SendEvent(new EntityBeforeDestroyedEvent {entity = entity});
        }

        private void HandleEntityRemoved(int index)
        {
            SendEvent(new EntityDestroyedCommand {index = index});
        }
    }
}