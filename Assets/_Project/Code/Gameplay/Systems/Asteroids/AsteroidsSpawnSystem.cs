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
            _t = 0;

            for (int i = 0; i < _settings.asteroids.startAmount; i++)
            {
                CreateAsteroid();
            }
        }

        protected override void OnUpdate(float deltaTime)
        {
            _t += deltaTime;

            if (_t > _settings.asteroids.delay)
            {
                _t = 0;

                if (_mask.count >= _settings.asteroids.maxAmount) return;

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
            AsteroidInfo info;

            if (index > 0)
            {
                info = _settings.asteroids.asteroidVariants[index];
            }
            else
            {
                RollByWeightUtility.TryRollByWeight(_settings.asteroids.asteroidVariants, out info);
            }

            CreateAsteroid(info, position);
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

                layer = CollisionLayer.ENEMY,
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

            entity.SetComponent(new ScoreComponent
            {
                value = info.score
            });

            entity.AddComponent<PortalTag, AsteroidTag, ParticleTag>();

            //Render
            entity.SetComponent(new SpriteRendererComponent
            {
                sprite = info.sprite,
            });
        }

        private bool GetRandomBool()
        {
            return Random.Range(0, 2) > 0;
        }
    }
}