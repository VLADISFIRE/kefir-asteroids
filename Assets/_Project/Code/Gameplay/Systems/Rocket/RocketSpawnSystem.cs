using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketSpawnSystem : BaseSystem
    {
        private GameSettingsScrobject _settings;
        private GameInput _gameInput;

        private Mask _mask;

        public RocketSpawnSystem(GameSettingsScrobject settings, GameInput gameInput)
        {
            _gameInput = gameInput;
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            CreateRocket();

            CreateAsteroid();

            Mask<AsteroidTag>().Build(out _mask);

            _gameInput.Enable();
        }

        protected override void OnDispose()
        {
            _gameInput.Disable();
        }

        private float _t;

        protected override void OnUpdate(float deltaTime)
        {
            _t += deltaTime;

            if (_t > 3)
            {
                _t = 0;

                if (_mask.count > 10) return;

                CreateAsteroid();
            }
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

        private void CreateAsteroid()
        {
            ref var asteroid = ref _world.NewEntity();

            asteroid.AddComponent<AsteroidTag>();
            asteroid.SetComponent(new TransformComponent
            {
                position = Vector2.zero,
                rotation = Vector2.up
            });

            var x = Random.Range(-2f, 2f);
            var y = Random.Range(-2f, 2f);

            asteroid.SetComponent(new MovementComponent
            {
                velocity = new Vector2(x, y)
            });

            asteroid.SetComponent(new SpriteRendererComponent
            {
                sprite = _settings.spawner.asteroid
            });
        }
    }
}