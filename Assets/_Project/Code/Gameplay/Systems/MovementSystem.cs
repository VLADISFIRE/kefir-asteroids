using Gameplay.Utility;
using Infrastructure.ECS;

namespace Gameplay
{
    public class MovementSystem : BaseSystem
    {
        private Mask _mask;

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

                transform.rotation.Rotate(movement.angleVelocity);
                transform.rotation.Normalize();
            }
        }
    }
}