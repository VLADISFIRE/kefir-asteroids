using System;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketObserverSystem : BaseSystem
    {
        private Mask _mask;

        private Vector2 _position;
        private Vector2 _rotation;

        private Vector2 _velocity;

        public Vector2 position { get { return _position; } }
        public Vector2 rotation { get { return _rotation; } }

        public Vector2 velocity { get { return _velocity; } }

        public event Action<Vector2> positionChanged;
        public event Action<Vector2> rotationChanged;

        public event Action<Vector2> velocityChanged;

        protected override void OnInitialize()
        {
            Mask<RocketTag, TransformComponent, MovementComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();
                ref var movement = ref entity.GetComponent<MovementComponent>();

                UpdatePosition(transform.position);
                UpdateRotation(transform.rotation);
                
                UpdateVelocity(movement.velocity);
            }
        }

        private void UpdatePosition(Vector2 value)
        {
            _position = value;
            positionChanged?.Invoke(value);
        }

        private void UpdateRotation(Vector2 value)
        {
            _rotation = value;
            rotationChanged?.Invoke(value);
        }

        private void UpdateVelocity(Vector2 value)
        {
            _velocity = value;
            velocityChanged?.Invoke(value);
        }
    }
}