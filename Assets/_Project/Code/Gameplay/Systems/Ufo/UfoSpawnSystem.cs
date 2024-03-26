using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay.Ufo
{
    public class UfoSpawnSystem : BaseSystem
    {
        private GameSettings _settings;
        private IScreen _screen;

        private Mask _mask;
        private Mask _rockets;

        private float _t;

        public UfoSpawnSystem(GameSettings settings, IScreen screen)
        {
            _settings = settings;
            _screen = screen;
        }

        protected override void OnInitialize()
        {
            Mask<UfoTag>().Build(out _mask);
            Mask<RocketTag>().Build(out _rockets);
        }

        protected override void OnPlayed()
        {
            _t = _settings.ufo.startDelay;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_mask.count >= _settings.ufo.maxAmount) return;

            _t -= deltaTime;

            if (_t <= 0)
            {
                _t = _settings.ufo.delay;

                CreateUfo();
            }
        }

        private void CreateUfo(Vector2? position = null)
        {
            if (_rockets.count <= 0) return;

            CreateUfo(_rockets[0], _settings.ufo.info, position);
        }

        private void CreateUfo(Entity followTarget, UfoInfo info, Vector2? position = null)
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

            entity.SetComponent(new TransformComponent
            {
                position = pos,
            });

            entity.SetComponent(new ColliderComponent
            {
                radius = info.radius,

                layer = CollisionLayer.ENEMY,
            });

            entity.AddComponent<MovementComponent>();

            entity.SetComponent(new HealthComponent
            {
                value = info.health,
                maxValue = info.health
            });

            entity.SetComponent(new FollowComponent
            {
                target = followTarget,
                speed = info.speed
            });

            entity.SetComponent(new DamageCollisionComponent
            {
                value = 1,
                autoDestroy = true
            });

            entity.SetComponent(new ScoreComponent
            {
                value = info.score
            });

            entity.AddComponent<PortalTag, UfoTag, ParticleTag>();

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