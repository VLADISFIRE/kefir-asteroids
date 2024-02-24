using System;

namespace Infrastructure.ECS
{
    /// <summary>
    /// Entity Component Holder (p.s manager)
    /// </summary>
    public class ComponentsHolder<T> : IComponentHolder
        where T : struct, IComponent
    {
        private const int DEFAULT_POOL_CAPACITY = 32;

        /// <summary>
        /// Entities
        /// </summary>
        private int[] _ids;

        private Pool<T> _pool;
        private int _hash;

        internal event Action<int, int> added;
        internal event Action<int, int> removed;

        internal ComponentsHolder(int capacity)
        {
            _ids = new int[capacity];
            _pool = new Pool<T>(DEFAULT_POOL_CAPACITY);
            _hash = ComponentIdentifier<T>.hash;
        }

        internal void Dispose()
        {
            _ids = null;
            _pool?.Dispose();
        }

        internal ref T Add(int id)
        {
            var index = GetIndex(id);

            if (Has(id))
                return ref _pool[index];

            index = InternalAdd(id);

            return ref _pool[index];
        }

        internal ref T Add(int id, out bool exist)
        {
            exist = false;
            var index = GetIndex(id);

            if (Has(id))
            {
                exist = true;
                return ref _pool[index];
            }

            index = InternalAdd(id);

            return ref _pool[index];
        }

        internal ref T Set(int id, T value)
        {
            var index = GetIndex(id);

            if (Has(id))
            {
                _pool.Set(index, value);
                return ref _pool[index];
            }

            index = InternalAdd(id, value);

            return ref _pool[index];
        }

        internal ref T Set(int id, T value, out bool exist)
        {
            exist = false;
            var index = GetIndex(id);

            if (Has(id))
            {
                exist = true;
                _pool.Set(index, value);
                return ref _pool[index];
            }

            index = InternalAdd(id, value);

            return ref _pool[index];
        }

        public ref T Get(int id)
        {
            return ref Add(id);
        }

        public bool Has(int id)
        {
            return GetIndex(id) > 0;
        }

        internal bool Remove(int id)
        {
            var x = Clear(id);
            removed?.Invoke(id, _hash);
            return x;
        }

        private bool Clear(int id)
        {
            if (!Has(id))
                return false;

            var index = GetIndex(id);
            _pool.Release(index);
            return false;
        }

        private int InternalAdd(int id, T value = default)
        {
            _pool.Get(out var index, value);
            SetIndex(id, index);
            added?.Invoke(id, _hash);
            return index;
        }

        void IComponentHolder.Resize(int capacity)
        {
            Array.Resize(ref _ids, capacity);
        }

        private void SetIndex(int id, int value)
        {
            _ids[id] = value + 1;
        }

        private int GetIndex(int id)
        {
            return _ids[id] - 1;
        }

        bool IComponentHolder.Remove(int id)
        {
            return Remove(id);
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        bool IComponentHolder.Clear(int id)
        {
            return Clear(id);
        }

        event Action<int, int> IComponentHolder.added { add { this.added += value; } remove { this.added -= value; } }
        event Action<int, int> IComponentHolder.removed { add { this.removed += value; } remove { this.removed -= value; } }
    }
}