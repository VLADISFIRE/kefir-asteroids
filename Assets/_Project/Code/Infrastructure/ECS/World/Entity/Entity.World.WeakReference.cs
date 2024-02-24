using System;

namespace Infrastructure.ECS
{
    public partial struct Entity
    {
        public WeakReference<World> world { get; internal set; }
    }
}