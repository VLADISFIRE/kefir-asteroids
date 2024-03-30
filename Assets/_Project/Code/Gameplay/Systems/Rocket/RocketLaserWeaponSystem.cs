using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketLaserWeaponSystem : BaseSystem
    {
        private Mask _mask;
        private Mask _fireMask;

        protected override void OnInitialize()
        {
            Mask<TransformComponent, RocketLaserComponent>().Build(out _mask);
            Mask<TransformComponent, RocketLaserComponent, RocketLaserFireEvent>().Build(out _fireMask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            UpdateCooldownAndCharge(deltaTime);
            Fire();
        }

        private void UpdateCooldownAndCharge(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var laser = ref entity.GetComponent<RocketLaserComponent>();
                if (laser.charges < laser.settings.charges)
                {
                    if (laser.newChargeCooldown > 0)
                    {
                        laser.newChargeCooldown -= deltaTime;
                    }
                    else
                    {
                        laser.charges++;

                        laser.newChargeCooldown = laser.charges < laser.settings.charges ? laser.settings.chargesCooldown : 0;
                    }
                }

                if (laser.charges <= 0) continue;

                if (laser.cooldown > 0)
                {
                    laser.cooldown -= deltaTime;
                }
            }
        }

        private void Fire()
        {
            foreach (var entity in _fireMask)
            {
                ref var laser = ref entity.GetComponent<RocketLaserComponent>();

                if (laser.cooldown > 0) continue;
                if (laser.charges <= 0) continue;

                ref var transform = ref entity.GetComponent<TransformComponent>();

                Fire(ref laser, transform.position, transform.rotation);
            }
        }

        private void Fire(ref RocketLaserComponent component, Vector2 position, Vector2 direction)
        {
            var spawnPosition = position + direction * component.settings.offset;
            CreateLaser(component.settings, spawnPosition, direction);

            if (component.charges >= component.settings.charges)
            {
                component.newChargeCooldown = component.settings.chargesCooldown;
            }

            component.charges--;

            component.cooldown = component.settings.cooldown;
        }

        private void CreateLaser(LaserSettings settings, Vector2 position, Vector2 direction)
        {
            ref var entity = ref _world.NewEntity();

            entity.SetComponent(new TransformComponent
            {
                position = position,
                rotation = direction
            });

            entity.SetComponent(new ColliderComponent
            {
                type = ColliderType.Line,

                size = settings.size.y,
                direction = direction,

                layer = CollisionLayer.ROCKET
            });

            entity.SetComponent(new SpriteRendererComponent
            {
                sprite = settings.sprite,
                size = settings.size
            });

            entity.SetComponent(new DamageCollisionComponent
            {
                value = settings.damage
            });

            entity.SetComponent(new DelayDestroyComponent
            {
                time = settings.cooldown
            });
        }
    }
}