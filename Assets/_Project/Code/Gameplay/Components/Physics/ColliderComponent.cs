using Infrastructure.ECS;

namespace Gameplay
{
    public struct ColliderComponent : IComponent
    {
        public float radius;

        public int layer;
    }
}