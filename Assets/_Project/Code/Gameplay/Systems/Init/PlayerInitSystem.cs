using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInitSystem : BaseSystem
    {
        private GameSettingsScrobject _settings;
        private GameInput _gameInput;

        public PlayerInitSystem(GameSettingsScrobject settings, GameInput gameInput)
        {
            _gameInput = gameInput;
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            ref var rocket = ref _world.NewEntity();

            rocket.AddComponent<RocketTag>();

            rocket.SetComponent(new TransformComponent
            {
                position = Vector2.zero,
                rotation = Vector2.up
            });

            rocket.AddComponent<RigidbodyComponent>();
            rocket.AddComponent<MovementComponent>();

            rocket.SetComponent(new RocketEngineComponent
            {
                power = _settings.player.rocket.enginePower,
            });
            rocket.SetComponent(new RocketRotateControlComponent()
            {
                speed = _settings.player.rocket.rotateSpeed,
            });

            _gameInput.Enable();
        }

        protected override void OnDispose()
        {
            _gameInput.Disable();
        }
    }
}