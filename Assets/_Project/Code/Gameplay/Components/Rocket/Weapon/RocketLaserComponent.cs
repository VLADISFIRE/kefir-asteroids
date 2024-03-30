using Infrastructure.ECS;

namespace Gameplay
{
    public struct RocketLaserComponent : IComponent
    {
        public LaserSettings settings;

        public float cooldown;
        public int charges;
        public float newChargeCooldown;
    }
}