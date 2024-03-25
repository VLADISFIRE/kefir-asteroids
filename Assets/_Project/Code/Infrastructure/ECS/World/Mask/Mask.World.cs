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
}