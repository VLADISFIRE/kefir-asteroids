using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public sealed partial class World
    {
        private const int DEFAULT_ENTITY_CAPACITY = 128;

        private Pool<Entity> _pool;

        private List<int> _entities;

        public ref Entity this[int index] { get { return ref _pool[index]; } }

        public event Action<Entity> beforeRemoved;
        public event Action<int> removed;

        public World(int capacity = DEFAULT_ENTITY_CAPACITY)
        {
            _pool = new Pool<Entity>(new Policy(), capacity);
            _pool.resized += OnResized;

            _entities = new List<int>(capacity);
        }

        private void DisposeEntity()
        {
            _pool.resized -= OnResized;

            _pool?.Dispose();
            _pool = null;
        }

        internal ref Entity NewEntity()
        {
            ref var entity = ref _pool.Get(out var index);

            entity.index = index;
            entity.world ??= new WeakReference<World>(this);

            _entities.Add(index);

            return ref _pool[index];
        }

        internal void RemoveEntity(int index)
        {
            ref var entity = ref _pool[index];

            beforeRemoved?.Invoke(entity);

            OnEntityRemovedByMask(index);
            OnEntityRemovedByComponentHolders(index);

            _entities.Remove(index);

            _pool.Release(index);

            removed?.Invoke(index);
        }

        internal void Clear()
        {
            OnClearByComponentsHolders();

            foreach (var index in _entities)
            {
                ref var entity = ref _pool[index];

                beforeRemoved?.Invoke(entity);
                
                _pool.Release(index);

                removed?.Invoke(index);
            }

            OnClearByMask();

            _entities.Clear();
        }

        internal bool IsAlive(int index)
        {
            return _pool[index].IsAlive();
        }

        private void OnResized(int capacity)
        {
            OnResizedByComponents(capacity);
        }

        private class Policy : IPoolPolicy<Entity>
        {
            public void OnGet(ref Entity obj)
            {
            }

            public void OnRelease(ref Entity obj)
            {
                obj.index = -1;
                obj.world = null;
            }
        }
    }
}