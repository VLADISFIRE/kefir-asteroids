using Infrastructure.ECS;

namespace Gameplay
{
    public struct RocketPistolComponent : IComponent
    {
        public PistolSettings settings;
        
        public float cooldown;
    }
}