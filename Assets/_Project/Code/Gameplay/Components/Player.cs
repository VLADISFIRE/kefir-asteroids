using Infrastructure.ECS;

namespace Gameplay
{
    public struct PlayerComponent : IComponent
    {
        public Entity rocket;

        public int score;
    }
}