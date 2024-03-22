using System;

namespace Infrastructure.ECS
{
    public class EcsManager : IEcsManager, IDisposable
    {
        private IEcsSystemFactory _systemFactory;
        private IEngineEvents _engineEvents;

        private Systems _systems;
        private World _world;

        public EcsManager(IEcsSystemFactory systemFactory, IEngineEvents engineEvents)
        {
            _systemFactory = systemFactory;
            _engineEvents = engineEvents;

            _world = new World();
            _systems = new Systems(_world);

            _systems.Initialize();

            _engineEvents.Subscribe(EventType.FixedUpdate, HandleUpdate);
        }

        public void Dispose()
        {
            _engineEvents?.Unsubscribe(EventType.FixedUpdate, HandleUpdate);

            _systems?.Dispose();
            _world?.Dispose();

            _systems = null;
            _world = null;
        }

        public IEcsManager AddSystem<T>() where T : BaseSystem
        {
            var instance = _systemFactory.Create<T>();
            _systems.AddSystem(instance);
            return this;
        }

        public IEcsManager RemoveSystem<T>() where T : BaseSystem
        {
            _systems.RemoveSystem<T>();
            return this;
        }

        private void HandleUpdate()
        {
            var deltaTime = _engineEvents.deltaTime;
            _systems.Update(deltaTime);
        }
    }
}