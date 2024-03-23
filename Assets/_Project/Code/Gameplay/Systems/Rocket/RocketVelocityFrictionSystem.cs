using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketVelocityFrictionSystem : BaseSystem
    {
        private const float ANGLE_VELOCITY_MODIFIER = 5;

        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<RocketEngineComponent, MovementComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var movement = ref entity.GetComponent<MovementComponent>();

                if (movement.velocity.magnitude != 0)
                    movement.velocity -= movement.velocity * deltaTime;

                if (movement.angleVelocity != 0)
                    movement.angleVelocity -= ANGLE_VELOCITY_MODIFIER * movement.angleVelocity * deltaTime;
            }
        }
    }
}