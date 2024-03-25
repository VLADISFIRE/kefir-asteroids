using Infrastructure.ECS;
using UnityEngine;
using Utility;

namespace Gameplay
{
    public class AsteroidsSpawnSystem : BaseEventSystem<AsteroidSpawnEvent>
    {
        private GameSettings _settings;
        private IScreen _screen;

        private Mask _mask;
        private Mask _spawnMask;

        private float _t;

        public AsteroidsSpawnSystem(GameSettings settings, IScreen screen)
        {
            _settings = settings;
            _screen = screen;
        }

        protected override void OnInitialized()
        {
            Mask<AsteroidTag>().Build(out _mask);
            Mask<AsteroidSpawnEvent>().Build(out _spawnMask);
        }

        protected override void OnPlayed()
        {
            for (int i = 0; i < _settings.spawner.startAsteroidsAmount; i++)
            {
                CreateAsteroid();
            }
        }

        private float _test;

        protected override void OnUpdate(float deltaTime)
        {
            _test += deltaTime;

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
                ref var asteroidsSpawn = ref entity.GetComponent<AsteroidsAfterDestroyComponent>();
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
            ref var entity = ref _world.NewEntity();

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

            entity.SetComponent(new TransformComponent
            {
                position = pos,
                rotation = rotation
            });

            entity.SetComponent(new ColliderComponent
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

            entity.SetComponent(new MovementComponent
            {
                velocity = velocity,
                angleVelocity = degreeAngleVelocity * Mathf.Deg2Rad
            });

            entity.SetComponent(new SpriteRendererComponent
            {
                sprite = info.sprite,
            });

            entity.SetComponent(new HealthComponent
            {
                value = info.health,
                maxValue = info.health
            });

            entity.SetComponent(new DamageCollisionComponent
            {
                value = 1
            });

            if (!info.asteroidsAfterDestroy.IsNullOrEmpty())
            {
                entity.SetComponent(new AsteroidsAfterDestroyComponent
                {
                    asteroids = info.asteroidsAfterDestroy
                });
            }

            entity.AddComponent<PortalTag, AsteroidTag, ParticleTag>();
        }

        private bool GetRandomBool()
        {
            return Random.Range(0, 2) > 0;
        }
    }
}