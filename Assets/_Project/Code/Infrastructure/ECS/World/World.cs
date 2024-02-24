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

        public World(int capacity = DEFAULT_ENTITY_CAPACITY)
        {
            _pool = new Pool<Entity>(capacity);
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
            _pool[index].index = -1;
            _pool[index].world = null;

            OnEntityRemovedByMask(index);
            OnEntityRemovedByComponentHolders(index);

            _entities.Remove(index);

            _pool.Release(index);
        }

        internal bool IsAlive(int index)
        {
            return _pool[index].index >= 0;
        }

        private void OnResized(int capacity)
        {
            OnResizedByComponents(capacity);
        }
    }
}