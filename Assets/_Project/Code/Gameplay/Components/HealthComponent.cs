using Infrastructure.ECS;

namespace Gameplay
{
    public struct HealthComponent : IComponent
    {
        public int value;
        public int maxValue;
    }
}