using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketFrictionSystem : BaseSystem
    {
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

                if (movement.velocity.magnitude <= 0) return;

                movement.velocity -= movement.velocity * deltaTime;
            }
        }
    }
}