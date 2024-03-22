using Infrastructure.ECS;

namespace Gameplay
{
    public struct RocketRotateControlComponent : IComponent
    {
        public bool enable;
        public bool left;
        
        public float speed;
    }
}