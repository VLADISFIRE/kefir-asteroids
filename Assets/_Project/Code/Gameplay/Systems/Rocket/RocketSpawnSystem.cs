using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketSpawnSystem : BaseSystem
    {
        private GameSettingsScrobject _settings;

        private Mask _mask;

        public RocketSpawnSystem(GameSettingsScrobject settings, GameInput gameInput)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            CreateRocket();

            Mask<AsteroidTag>().Build(out _mask);
        }

        private void CreateRocket()
        {
            ref var rocket = ref _world.NewEntity();

            rocket.SetComponent(new TransformComponent
            {
                position = Vector2.zero,
                rotation = Vector2.up
            });

            rocket.AddComponent<RigidbodyComponent>();
            rocket.AddComponent<MovementComponent>();
            rocket.SetComponent(new CollisionComponent
            {
                radius = 0.5f
            });

            rocket.SetComponent(new RocketEngineComponent
            {
                power = _settings.player.rocket.enginePower,
            });
            rocket.SetComponent(new RocketRotateControlComponent()
            {
                speed = _settings.player.rocket.rotateSpeed,
            });

            rocket.SetComponent(new SpriteRendererComponent
            {
                sprite = _settings.player.sprite
            });
        }
    }
}