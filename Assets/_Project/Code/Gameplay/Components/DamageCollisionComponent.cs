using Infrastructure.ECS;

namespace Gameplay
{
    public struct DamageCollisionComponent : IComponent
    {
        public int value;
        public bool autoDestroy;
    }
}