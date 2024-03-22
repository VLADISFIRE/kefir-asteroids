using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketAngleVelocityFrictionSystem : BaseSystem
    {
        private const float FRICTION_POWER = 5;

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
                
                if (movement.angleVelocity != 0)
                    movement.angleVelocity -= FRICTION_POWER * movement.angleVelocity * deltaTime;
            }
        }
    }
}