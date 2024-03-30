using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketPistolWeaponSystem : BaseSystem
    {
        private Mask _mask;
        private Mask _fireMask;

        protected override void OnInitialize()
        {
            Mask<TransformComponent, RocketPistolComponent>().Build(out _mask);
            Mask<TransformComponent, RocketPistolComponent, RocketPistolFireEvent>().Build(out _fireMask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            UpdateCooldown(deltaTime);
            Fire();
        }

        private void UpdateCooldown(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var pistol = ref entity.GetComponent<RocketPistolComponent>();

                if (pistol.cooldown > 0)
                {
                    pistol.cooldown -= deltaTime;
                }
            }
        }

        private void Fire()
        {
            foreach (var entity in _fireMask)
            {
                ref var pistol = ref entity.GetComponent<RocketPistolComponent>();

                if (pistol.cooldown > 0) continue;

                ref var transform = ref entity.GetComponent<TransformComponent>();
                Fire(ref pistol, transform.position, transform.rotation);
            }
        }

        private void Fire(ref RocketPistolComponent component, Vector2 position, Vector2 direction)
        {
            var spawnPosition = position + direction * component.settings.offset;
            CreateBullet(component.settings, spawnPosition, direction);

            component.cooldown = component.settings.cooldown;
        }

        private void CreateBullet(PistolSettings settings, Vector2 position, Vector2 direction)
        {
            ref var bullet = ref _world.NewEntity();

            bullet.SetComponent(new TransformComponent
            {
                position = position
            });

            bullet.SetComponent(new ColliderComponent
            {
                radius = settings.radius,

                layer = CollisionLayer.ROCKET
            });

            var velocity = direction * settings.speed;
            bullet.SetComponent(new MovementComponent
            {
                velocity = velocity
            });

            bullet.SetComponent(new SpriteRendererComponent
            {
                sprite = settings.sprite
            });

            bullet.SetComponent(new DamageCollisionComponent
            {
                value = 1,
                autoDestroy = true
            });
        }
    }
}