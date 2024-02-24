using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.ECS
{
    public sealed partial class World
    {
        private const int DEFAULT_HOLDERS_CAPACITY = 32;

        private readonly Dictionary<int, IComponentHolder> _holders = new(DEFAULT_HOLDERS_CAPACITY);

        private void DisposeComponents()
        {
            foreach (var holder in _holders.Values)
            {
                holder.added -= OnComponentAdded;
                holder.removed -= OnComponentRemoved;
                holder.Dispose();
            }

            _holders.Clear();
        }

        internal ref T Add<T>(int id) where T : struct, IComponent
        {
            ValidateEntity<T>(id);

            var holder = GetHolder<T>();
            return ref holder.Add(id);
        }

        internal ref T Add<T>(int id, out bool exist) where T : struct, IComponent
        {
            ValidateEntity<T>(id);

            var holder = GetHolder<T>();
            return ref holder.Add(id, out exist);
        }

        public ref T Set<T>(int id, T value) where T : struct, IComponent
        {
            ValidateEntity<T>(id);

            var holder = GetHolder<T>();
            return ref holder.Set(id, value);
        }

        public ref T Set<T>(int id, T value, out bool exist) where T : struct, IComponent
        {
            ValidateEntity<T>(id);

            var holder = GetHolder<T>();
            return ref holder.Set(id, value, out exist);
        }

        internal ref T Get<T>(int id) where T : struct, IComponent
        {
            ValidateEntity<T>(id);

            var holder = GetHolder<T>();
            return ref holder.Get(id);
        }

        internal bool Has<T>(int id) where T : struct, IComponent
        {
            ValidateEntity<T>(id);

            var holder = GetHolder<T>();
            return holder.Has(id);
        }

        internal bool Has(int id, int hash)
        {
            ValidateEntity(id, hash);

            if (!_holders.TryGetValue(hash, out var holder))
                return false;

            return holder.Has(id);
        }

        internal bool Remove<T>(int id) where T : struct, IComponent
        {
            var holder = GetHolder<T>();
            return holder.Remove(id);
        }

        public ComponentsHolder<T> GetHolder<T>() where T : struct, IComponent
        {
            var hash = ComponentIdentifier<T>.hash;

            ComponentsHolder<T> holder;
            if (_holders.TryGetValue(hash, out var value))
            {
                holder = value as ComponentsHolder<T>;
            }
            else
            {
                holder = Create<T>();
                _holders[hash] = holder;
            }

            return holder;
        }

        private ComponentsHolder<T> Create<T>() where T : struct, IComponent
        {
            var capacity = _pool.capacity;
            var holder = new ComponentsHolder<T>(capacity);

            holder.added += OnComponentAdded;
            holder.removed += OnComponentRemoved;

            return holder;
        }

        private void OnComponentAdded(int id, int hash)
        {
            OnComponentAddedByMask(id, hash);
        }

        private void OnComponentRemoved(int id, int hash)
        {
            OnComponentRemovedByMask(id, hash);
        }

        private void OnEntityRemovedByComponentHolders(int id)
        {
            foreach (var holder in _holders.Values)
            {
                holder.Clear(id);
            }
        }

        private void OnResizedByComponents(int capacity)
        {
            foreach (var holder in _holders.Values)
            {
                holder.Resize(capacity);
            }
        }

        private void ValidateEntity<T>(int id) where T : struct, IComponent
        {
            if (_pool.capacity <= id)
                throw new Exception($"Attempt to add a component by type [ {typeof(T)} ] to a non-existent entity by id [ {id} ]");

            if (!_pool[id].IsAlive())
                throw new Exception($"Attempt to add a component by type [ {typeof(T)} ] to a non-alive entity by id [ {id} ]");
        }

        private void ValidateEntity(int id, int hash)
        {
            if (_pool.capacity <= id)
                throw new Exception($"Attempt to add a component by hash [ {hash} ] to a non-existent entity by id [ {id} ]");

            if (!_pool[id].IsAlive())
                throw new Exception($"Attempt to add a component by hash [ {hash} ] to a non-alive entity by id [ {id} ]");
        }
    }
}