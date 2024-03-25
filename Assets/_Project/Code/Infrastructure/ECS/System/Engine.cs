using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public partial class Engine : IDisposable
    {
        private const int CAPACITY = 16;

        private World _world;
        private ISystemFactory _factory;

        private Dictionary<int, ISystem> _dictionary = new(CAPACITY);

        private List<ISystem> _activeSystems = new(CAPACITY);

        /// <summary>
        /// Collection for OnPlayed invoke
        /// </summary>
        private List<ISystem> _waitingSystems = new(CAPACITY);

        private bool _initialized;
        private bool _play;

        public IReadOnlyCollection<ISystem> systems { get { return _dictionary.Values; } }
        public IReadOnlyCollection<ISystem> activeSystems { get { return _activeSystems; } }

        public bool isPlaying { get { return _play; } }

        public Engine(World world, ISystemFactory factory)
        {
            _factory = factory;
            _world = world;
        }

        public void Dispose()
        {
            _world = null;

            foreach (var system in systems)
            {
                system?.Dispose();
            }

            _dictionary.Clear();
            _dictionary = null;

            DisposeEvents();
        }

        internal void Initialize(bool autoPlay = true)
        {
            _initialized = true;

            foreach (var system in _activeSystems)
            {
                system.Initialize(_world);
            }

            if (autoPlay)
                PlayAll();
        }

        internal T Add<T>(bool autoPlay = true) where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            if (_dictionary.TryGetValue(hash, out var value))
                return (T) value;

            var system = _factory.Create<T>();
            _dictionary.Add(hash, system);

            if (autoPlay)
                Play(system);

            OnSystemAddedByEvent(system);

            return system;
        }

        internal void Remove<T>() where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            if (_dictionary.TryGetValue(hash, out var system))
            {
                Remove(system, hash);
            }

            OnSystemRemovedByEvent(system, hash);
        }

        internal void Update(float deltaTime)
        {
            if (!_play) return;

            OnBeforeUpdate();

            foreach (var system in _activeSystems)
            {
                system?.Update(deltaTime);
            }

            OnAfterUpdate();
        }

        internal void PlayAll()
        {
            _play = true;
        }

        internal void StopAll()
        {
            _play = false;

            foreach (var system in _activeSystems)
            {
                system.OnStopped();
            }
        }

        internal void Play<T>() where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;
            var system = _dictionary[hash];

            Play(system);
        }

        private void Play(ISystem system)
        {
            _activeSystems.Add(system);

            if (_initialized)
                system.Initialize(_world);

            _waitingSystems.Add(system);
        }

        internal void Stop<T>() where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;
            var system = _dictionary[hash];

            Stop(system);
        }

        private void Stop(ISystem system)
        {
            _activeSystems.Remove(system);

            system.OnStopped();
        }

        internal bool IsPlay<T>() where T : ISystem
        {
            var hash = SystemIdentifier<T>.hash;

            var system = _dictionary[hash];
            return IsPlay(system);
        }

        private bool IsPlay<T>(T system) where T : ISystem
        {
            return _activeSystems.Contains(system);
        }

        private void Remove(ISystem system, int hash)
        {
            _dictionary.Remove(hash);
        }

        private void OnBeforeUpdate()
        {
            if (_waitingSystems.Count <= 0) return;

            foreach (var system in _waitingSystems)
            {
                system.OnPlayed();
            }

            _waitingSystems.Clear();
        }

        private void OnAfterUpdate()
        {
            foreach (var system in _activeSystems)
            {
                system?.LateUpdate();
            }
        }
    }
}