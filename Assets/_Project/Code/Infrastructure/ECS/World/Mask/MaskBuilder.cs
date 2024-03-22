using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public struct MaskBuilder
    {
        private const int DEFAULT_INCLUDE_CAPACITY = 8;
        private const int DEFAULT_EXCLUDE_CAPACITY = 4;

        private readonly World _world;

        private readonly HashSet<int> _include;
        private readonly HashSet<int> _exclude;

        public long hash { get; private set; }

        internal MaskBuilder(World world)
        {
            _world = world;

            _include = new HashSet<int>(DEFAULT_INCLUDE_CAPACITY);
            _exclude = new HashSet<int>(DEFAULT_EXCLUDE_CAPACITY);

            hash = 0;
        }

        internal Mask Build()
        {
            CalculateHash();

            if (_world.TryGetMask(hash, out var mask))
                return mask;

            mask = new Mask(_world, _include, _exclude);
            _world.Add(hash, mask);

            return mask;
        }

        internal MaskBuilder Include<T>() where T : IComponent
        {
            var componentHash = ComponentIdentifier<T>.hash;
            _include.Add(componentHash);

            return this;
        }

        internal MaskBuilder Exclude<T>() where T : IComponent
        {
            var componentHash = ComponentIdentifier<T>.hash;
            _exclude.Add(componentHash);

            return this;
        }

        private void CalculateHash()
        {
            foreach (var i in _include)
            {
                hash += i;
            }

            foreach (var e in _exclude)
            {
                hash -= e;
            }
        }
    }
}