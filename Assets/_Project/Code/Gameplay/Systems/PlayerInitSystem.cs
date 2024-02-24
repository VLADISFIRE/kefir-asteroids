using Game.Gameplay;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInitSystem : BaseSystem
    {
        private GameSettingsScrobject _settings;

        public PlayerInitSystem(GameSettingsScrobject settings)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            ref var player = ref _world.NewEntity();

            player.AddComponent<PlayerTag>();
            
            player.AddComponent<MovementComponent>();
            player.SetComponent(_settings.player.speed);
            
            player.SetComponent(new TransformComponent
            {
                position = Vector2.zero
            });
        }
    }

    public struct MovementComponent : IComponent
    {
        public Vector2 velocity;
    }
}