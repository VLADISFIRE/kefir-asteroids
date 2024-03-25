using Infrastructure.ECS;

namespace Gameplay
{
    public struct AsteroidSpawnEvent : IEvent
    {
        public int[] asteroids;
    }
}