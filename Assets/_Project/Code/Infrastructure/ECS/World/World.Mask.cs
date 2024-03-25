using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public sealed partial class World
    {
        private const int DEFAULT_MASK_CAPACITY = 32;

        private Dictionary<long, Mask> _masks = new(DEFAULT_MASK_CAPACITY);

        private void DisposeMasks()
        {
            _masks.Clear();
            _masks = null;
        }

        internal void Add(long key, Mask mask)
        {
            _masks.Add(key, mask);

            FillMask(mask);
        }

        internal bool TryGetMask(long hash, out Mask mask)
        {
            return _masks.TryGetValue(hash, out mask);
        }

        private void FillMask(Mask mask)
        {
            foreach (var id in _entities)
            {
                if (!this.Check(id, mask.include, mask.exclude)) continue;

                mask.Add(id);
            }
        }

        private void OnComponentAddedByMask(int index, int hash)
        {
            foreach (var mask in _masks.Values)
            {
                mask.OnComponentAdded(index, hash);
            }
        }

        private void OnComponentRemovedByMask(int index, int hash)
        {
            foreach (var mask in _masks.Values)
            {
                mask.OnComponentRemoved(index, hash);
            }
        }

        private void OnEntityRemovedByMask(int index)
        {
            foreach (var mask in _masks.Values)
            {
                mask.Remove(index);
            }
        }

        private void OnClearByMask()
        {
            foreach (var mask in _masks.Values)
            {
                mask.Clear();
            }
        }
    }
}