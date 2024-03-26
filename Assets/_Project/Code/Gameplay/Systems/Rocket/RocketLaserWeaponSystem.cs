using System;
using Gameplay.Utility;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketLaserWeaponSystem : BaseSystem
    {
        private Mask _mask;
        private GameSettings _settings;

        private float _t;

        private float _cooldown;
        private int _charges;

        public int charges { get { return _charges; } }
        public float cooldown { get { return _cooldown; } }

        public event Action<int> chargesChanged;
        public event Action<float> cooldownChanged;

        public RocketLaserWeaponSystem(GameSettings settings)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent, RocketLaserFireEvent>().Build(out _mask);
        }

        protected override void OnPlayed()
        {
            _t = 0;
            _cooldown = 0;
            _charges = _settings.rocket.weapon.laser.charges;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_charges < _settings.rocket.weapon.laser.charges)
            {
                if (_cooldown > 0)
                {
                    RemoveCooldown(deltaTime);
                }
                else
                {
                    AddCharge();

                    if (_charges < _settings.rocket.weapon.laser.charges)
                        UpdateCooldown(_settings.rocket.weapon.laser.chargesCooldown);
                }
            }

            if (_charges <= 0) return;

            if (_t > 0)
            {
                _t -= deltaTime;
                return;
            }

            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();

                Fire(transform.position, transform.rotation);
            }
        }

        private void Fire(Vector2 position, Vector2 direction)
        {
            var spawnPosition = position + direction * _settings.rocket.weapon.laser.offset;
            CreateLaser(spawnPosition, direction);

            if (_charges >= _settings.rocket.weapon.laser.charges)
                UpdateCooldown(_settings.rocket.weapon.laser.chargesCooldown);

            RemoveCharge();

            _t = _settings.rocket.weapon.laser.cooldown;
        }

        private void CreateLaser(Vector2 position, Vector2 direction)
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

                size = _settings.rocket.weapon.laser.size.y,
                direction = direction,

                layer = CollisionLayer.ROCKET
            });

            entity.SetComponent(new SpriteRendererComponent
            {
                sprite = _settings.rocket.weapon.laser.sprite,
                size = _settings.rocket.weapon.laser.size
            });

            entity.SetComponent(new DamageCollisionComponent
            {
                value = _settings.rocket.weapon.laser.damage
            });

            entity.SetComponent(new DelayDestroyComponent
            {
                time = _settings.rocket.weapon.laser.cooldown
            });
        }

        private void AddCharge()
        {
            UpdateCharges(_charges + 1);
        }

        private void RemoveCharge()
        {
            UpdateCharges(_charges - 1);
        }

        private void UpdateCharges(int value)
        {
            _charges = value;
            chargesChanged?.Invoke(value);
        }

        private void RemoveCooldown(float deltaTime)
        {
            UpdateCooldown(_cooldown - deltaTime);
        }

        private void UpdateCooldown(float value)
        {
            _cooldown = Mathf.Clamp(value, 0, value);
            cooldownChanged?.Invoke(_cooldown);
        }
    }
}