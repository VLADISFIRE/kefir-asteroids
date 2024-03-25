using Infrastructure.ECS;
using UnityEngine;
using Utility;

namespace Gameplay
{
    public class AsteroidsSpawnerSystem : BaseSystem
    {
        private GameSettings _settings;
        private IScreen _screen;

        private Mask _mask;
        private Mask _spawnMask;
        private float _t;

        public AsteroidsSpawnerSystem(GameSettings settings, IScreen screen)
        {
            _settings = settings;
            _screen = screen;
        }

        protected override void OnInitialize()
        {
            Mask<AsteroidTag>().Build(out _mask);
            Mask<AsteroidSpawnEvent>().Build(out _spawnMask);
        }

        protected override void OnPlayed()
        {
            for (int i = 0; i < _settings.spawner.startAsteroidsAmount; i++)
            {
                CreateAsteroid(AsteroidType.BIG);
            }
        }

        protected override void OnUpdate(float deltaTime)
        {
            _t += deltaTime;

            if (_t > _settings.spawner.delay)
            {
                _t = 0;

                if (_mask.count >= _settings.spawner.maxAsteroids) return;

                CreateAsteroid();
            }
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _spawnMask)
            {
                ref var asteroidsSpawn = ref entity.GetComponent<AsteroidsSpawnComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                foreach (var index in asteroidsSpawn.asteroids)
                {
                    CreateAsteroid(index, transform.position);
                }
            }
        }

        private void CreateAsteroid(int index = -1, Vector2? position = null)
        {
            if (index < 0)
            {
                index = Random.Range(0, _settings.spawner.asteroids.Length);
            }

            var asteroidInfo = _settings.spawner.asteroids[index];
            CreateAsteroid(asteroidInfo, position);
        }

        private void CreateAsteroid(AsteroidInfo info, Vector2? position = null)
        {
            ref var asteroid = ref _world.NewEntity();

            Vector2 pos;
            if (position != null)
            {
                pos = position.Value;
            }
            else
            {
                var x = Random.Range(_screen.lowerLeft.x, _screen.topRight.x);
                var y = GetRandomBool() ? _screen.lowerLeft.y : _screen.topRight.y;

                pos = new Vector2(x, y);
            }

            var x2 = Random.Range(-1f, 1f);
            var y2 = Random.Range(-1f, 1f);

            var rotation = new Vector2(x2, y2).normalized;

            asteroid.SetComponent(new TransformComponent
            {
                position = pos,
                rotation = rotation
            });

            asteroid.SetComponent(new ColliderComponent
            {
                radius = info.radius,

                layer = CollisionLayer.ASTEROID,
            });

            var speed = Random.Range(info.minSpeed, info.maxSpeed);
            var degreeAngleVelocity = Random.Range(speed / 4, speed);

            if (GetRandomBool())
            {
                degreeAngleVelocity = -degreeAngleVelocity;
            }

            var velocity = rotation * speed;

            asteroid.SetComponent(new MovementComponent
            {
                velocity = velocity,
                angleVelocity = degreeAngleVelocity * Mathf.Deg2Rad
            });

            asteroid.SetComponent(new SpriteRendererComponent
            {
                sprite = info.sprite,
            });

            asteroid.SetComponent(new HealthComponent
            {
                value = info.health,
                maxValue = info.health
            });

            asteroid.SetComponent(new DamageCollisionComponent
            {
                value = 1
            });

            if (!info.asteroidsAfterDestroy.IsNullOrEmpty())
            {
                asteroid.SetComponent(new AsteroidsSpawnComponent
                {
                    asteroids = info.asteroidsAfterDestroy
                });
            }

            asteroid.AddComponent<PortalTag, AsteroidTag>();
        }

        private bool GetRandomBool()
        {
            return Random.Range(0, 2) > 0;
        }
    }
}