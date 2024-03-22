using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public class Systems : IDisposable
    {
        private World _world;

        private List<ISystem> _systems = new(16);

        private Dictionary<int, ISystem> _dictionary = new(16);

        private bool _initialized;

        public Systems(World world)
        {
            _world = world;
        }

        public void Dispose()
        {
            foreach (var system in _systems)
            {
                system?.Dispose();
            }

            _systems.Clear();
            _systems = null;

            _dictionary.Clear();
            _dictionary = null;
            
        }

        internal void Initialize()
        {
            _initialized = true;

            foreach (var system in _systems)
            {
                system.Initialize(_world);
            }
        }

        internal T Add<T>(T system) where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            if (_dictionary.TryGetValue(hash, out var value))
                return (T) value;

            _dictionary.Add(hash, system);
            _systems.Add(system);

            if (_initialized)
            {
                system.Initialize(_world);
            }

            return system;
        }

        internal void Remove<T>() where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            if (_dictionary.TryGetValue(hash, out var system))
            {
                Remove(system);
            }
        }

        internal void Remove<T>(T system) where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            _dictionary.Remove(hash);
            _systems.Remove(system);
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
    }
}