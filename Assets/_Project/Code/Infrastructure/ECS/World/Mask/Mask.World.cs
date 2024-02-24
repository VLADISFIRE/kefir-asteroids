using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public sealed partial class Mask
    {
        private World _world;

        internal Mask(World world, HashSet<int> include, HashSet<int> exclude) : this(include, exclude)
        {
            _world = world;
        }
    }

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
            foreach (var entity in _entities)
            {
                foreach (var includeHash in mask.include)
                {
                    if (!_holders.TryGetValue(includeHash, out var holder))
                        continue;
                }

                foreach (var excludeHash in mask.exclude)
                {
                    if (_holders.TryGetValue(excludeHash, out var holder))
                        continue;
                }

                mask.Add(entity);
            }
        }
    }
}