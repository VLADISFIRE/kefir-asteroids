using Infrastructure.ECS;

namespace Gameplay
{
    public struct FollowComponent : IComponent
    {
        public Entity target;

        public float speed;
    }
}