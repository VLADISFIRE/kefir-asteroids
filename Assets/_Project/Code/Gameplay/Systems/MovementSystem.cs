using Gameplay.Utility;
using Infrastructure.ECS;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Gameplay
{
    public class MovementSystem : BaseSystem
    {
        private GameSettingsScrobject _settings;

        private Mask _mask;

        public MovementSystem(GameSettingsScrobject settings)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent, MovementComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();
                ref var movement = ref entity.GetComponent<MovementComponent>();

                transform.position += movement.velocity * deltaTime;

                transform.rotation = transform.rotation.Rotate(movement.angleVelocity);
                transform.rotation.Normalize();
                
            }
        }
    }
}