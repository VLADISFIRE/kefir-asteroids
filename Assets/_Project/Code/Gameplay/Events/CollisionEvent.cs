using Infrastructure.ECS;

namespace Gameplay
{
    public struct CollisionEvent : IEvent
    {
        public Entity entity;
    }
}