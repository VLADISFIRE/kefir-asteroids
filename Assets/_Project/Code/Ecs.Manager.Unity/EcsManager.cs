using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public class EcsManager : IEcsManager, IDisposable
    {
        private IEngineEvents _engineEvents;

        private World _world;
        private Dictionary<EngineType, Engine> _engines = new(2);

        private EngineType _defaultType;

        public EcsManager(IEngineEvents engineEvents, ISystemFactory factory)
        {
            _engineEvents = engineEvents;

            _world = new World();
            _engines.Add(EngineType.Default, new Engine(_world, factory));
            _engines.Add(EngineType.Fixed, new Engine(_world, factory));

            foreach (var engine in _engines.Values)
            {
                engine.Initialize();
            }

            _engineEvents.Subscribe(EventType.Update, HandleUpdate);
            _engineEvents.Subscribe(EventType.FixedUpdate, HandleFixedUpdate);
        }

        public void Dispose()
        {
            _engineEvents?.Unsubscribe(EventType.Update, HandleUpdate);
            _engineEvents?.Unsubscribe(EventType.FixedUpdate, HandleFixedUpdate);

            foreach (var engine in _engines.Values)
            {
                engine.Dispose();
            }

            _engines.Clear();
            _engines = null;

            _world = null;
        }

        public IEcsManager AddSystem<T>(bool active = true) where T : BaseSystem
        {
            return AddSystem<T>(_defaultType);
        }

        public IEcsManager AddSystem<T>(EngineType type, bool active = true) where T : BaseSystem
        {
            _engines[type].AddSystem<T>(active);
            return this;
        }

        public IEcsManager RemoveSystem<T>() where T : BaseSystem
        {
            return RemoveSystem<T>(_defaultType);
        }

        public IEcsManager RemoveSystem<T>(EngineType type) where T : BaseSystem
        {
            _engines[type].RemoveSystem<T>();
            return this;
        }

        public bool IsPlaying(EngineType type)
        {
            if (_engines.TryGetValue(type, out var engine))
            {
                return engine.isPlaying;
            }

            return false;
        }

        public void SetDefaultType(EngineType type)
        {
            _defaultType = type;
        }

        public void PlayAll()
        {
            foreach (var engine in _engines.Values)
            {
                engine.PlayAll();
            }
        }

        public void PlayAll(EngineType type)
        {
            _engines[type].PlayAll();
        }

        public void Play<T>(EngineType type) where T : BaseSystem
        {
            _engines[type].Play<T>();
        }

        public void StopAll()
        {
            foreach (var engine in _engines.Values)
            {
                engine.StopAll();
            }
        }

        public void StopAll(EngineType type)
        {
            _engines[type].StopAll();
        }

        public void Stop<T>(EngineType type) where T : BaseSystem
        {
            _engines[type].Stop<T>();
        }

        private void HandleUpdate()
        {
            var deltaTime = _engineEvents.GetDeltaTime(EventType.Update);
            _engines[EngineType.Default].Update(deltaTime);
        }

        private void HandleFixedUpdate()
        {
            var deltaTime = _engineEvents.GetDeltaTime(EventType.FixedUpdate);
            _engines[EngineType.Fixed].Update(deltaTime);
        }
    }
}