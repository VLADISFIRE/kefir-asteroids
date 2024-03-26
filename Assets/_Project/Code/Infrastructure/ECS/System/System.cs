using System;

namespace Infrastructure.ECS
{
    public interface ISystem : IDisposable
    {
        public void Initialize(World world);

        public void BeforeUpdate();
        public void Update(float deltaTime);
        public void LateUpdate();

        public void OnPlayed();
        public void OnStopped();

        internal void SystemUpdate();
    }

    public abstract partial class BaseSystem : ISystem
    {
        protected World _world;

        public bool initialized { get { return _world != null; } }

        public void Dispose()
        {
            _world = null;

            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        void ISystem.BeforeUpdate()
        {
            if (!initialized) return;

            OnBeforeUpdate();
        }

        protected virtual void OnBeforeUpdate()
        {
        }

        void ISystem.Initialize(World world)
        {
            if (_world == world) return;

            _world = world;

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        void ISystem.Update(float deltaTime)
        {
            if (!initialized) return;

            OnUpdate(deltaTime);
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        void ISystem.SystemUpdate()
        {
            if (!initialized) return;

            OnSystemUpdate();
        }

        protected virtual void OnSystemUpdate()
        {
        }

        void ISystem.LateUpdate()
        {
            if (!initialized) return;

            OnLateUpdate();
        }

        protected virtual void OnLateUpdate()
        {
        }

        void ISystem.OnPlayed()
        {
            if (!initialized) return;

            OnPlayed();
        }

        protected virtual void OnPlayed()
        {
        }

        void ISystem.OnStopped()
        {
            if (!initialized) return;

            OnStopped();
        }

        protected virtual void OnStopped()
        {
        }
    }
}