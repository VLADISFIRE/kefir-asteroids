using Infrastructure.ECS;

namespace Gameplay
{
    public struct RocketEngineComponent : IComponent
    {
        public bool enable;
        public float power;
    }
}