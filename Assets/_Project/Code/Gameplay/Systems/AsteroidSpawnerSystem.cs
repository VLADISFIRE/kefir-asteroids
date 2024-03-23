using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class AsteroidSpawnerSystem : BaseSystem
    {
        private GameSettings _settings;
        private IScreen _screen;

        private Mask _mask;
        private float _t;

        public AsteroidSpawnerSystem(GameSettings settings, IScreen screen)
        {
            _settings = settings;
            _screen = screen;
        }

        protected override void OnInitialize()
        {
            Mask<EnemyTag>().Build(out _mask);
        }

        protected override void OnPlayed()
        {
            for (int i = 0; i < 5; i++)
            {
                CreateBigAsteroid();
            }
        }

        protected override void OnUpdate(float deltaTime)
        {
            _t += deltaTime;

            if (_t > 3)
            {
                _t = 0;

                if (_mask.count > 10) return;

                CreateBigAsteroid();
                CreateSmallAsteroid();
            }
        }

        private void CreateBigAsteroid()
        {
            ref var asteroid = ref _world.NewEntity();

            var x = Random.Range(_screen.lowerLeft.x, _screen.topRight.x);
            var y = Random.Range(0, 2) >= 1 ? _screen.lowerLeft.y : _screen.topRight.y;

            var x2 = Random.Range(-1, 1);
            var y2 = Random.Range(-1, 1);

            asteroid.SetComponent(new TransformComponent
            {
                position = new Vector2(x, y),
                rotation = new Vector2(x2, y2).normalized
            });
            asteroid.SetComponent(new CollisionComponent
            {
                radius = 0.5f,

                layer = CollisionLayer.ASTEROID
            });

            var degreeAngleVelocity = Random.Range(-5f, 5f);

            var x3 = Random.Range(-2, 2);
            var y3 = Random.Range(-2, 2);

            asteroid.SetComponent(new MovementComponent
            {
                velocity = new Vector2(x3, y3),
                angleVelocity = degreeAngleVelocity * Mathf.Deg2Rad
            });

            asteroid.SetComponent(new SpriteRendererComponent
            {
                sprite = _settings.spawner.asteroid
            });

            asteroid.AddComponent<PortalTag, EnemyTag>();
        }

        private void CreateSmallAsteroid()
        {
            ref var asteroid = ref _world.NewEntity();

            var x = Random.Range(_screen.lowerLeft.x, _screen.topRight.x);
            var y = Random.Range(0, 2) >= 1 ? _screen.lowerLeft.y : _screen.topRight.y;

            var x2 = Random.Range(-1, 1);
            var y2 = Random.Range(-1, 1);

            asteroid.SetComponent(new TransformComponent
            {
                position = new Vector2(x, y),
                rotation = new Vector2(x2, y2).normalized
            });
            asteroid.SetComponent(new CollisionComponent
            {
                radius = 0.25f,

                layer = CollisionLayer.ASTEROID,
            });

            var degreeAngleVelocity = Random.Range(-5f, 5f);

            var x3 = Random.Range(-2, 2);
            var y3 = Random.Range(-2, 2);

            asteroid.SetComponent(new MovementComponent
            {
                velocity = new Vector2(x3, y3),
                angleVelocity = degreeAngleVelocity * Mathf.Deg2Rad
            });

            asteroid.SetComponent(new SpriteRendererComponent
            {
                sprite = _settings.spawner.smallAsteroid
            });

            asteroid.AddComponent<PortalTag, EnemyTag>();
        }
    }
}