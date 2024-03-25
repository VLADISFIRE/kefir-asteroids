using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketPistolWeaponSystem : BaseSystem
    {
        private Mask _mask;
        private GameSettings _settings;

        private float _t;

        public RocketPistolWeaponSystem(GameSettings settings)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent, RocketWeaponFireEvent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_t > 0)
            {
                _t -= deltaTime;
                return;
            }

            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();
                ref var eventData = ref entity.GetComponent<RocketWeaponFireEvent>();

                if (eventData.type == RocketWeaponType.BULLET)
                {
                    var position = transform.position + transform.rotation * _settings.rocket.weapon.pistol.offset;
                    CreateBullet(position, transform.rotation);

                    _t = _settings.rocket.weapon.pistol.cooldown;
                }
            }
        }

        private void CreateBullet(Vector2 position, Vector2 direction)
        {
            ref var bullet = ref _world.NewEntity();

            bullet.SetComponent(new TransformComponent
            {
                position = position
            });

            bullet.SetComponent(new ColliderComponent
            {
                radius = _settings.rocket.weapon.pistol.radius,
            });

            var velocity = direction * _settings.rocket.weapon.pistol.speed;
            bullet.SetComponent(new MovementComponent
            {
                velocity = velocity
            });

            bullet.SetComponent(new SpriteRendererComponent
            {
                sprite = _settings.rocket.weapon.pistol.sprite
            });

            bullet.SetComponent(new DamageCollisionComponent
            {
                value = 1,
                autoDestroy = true
            });
        }
    }
}