using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketFrictionSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<RocketTag, MovementComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var velocity = ref entity.GetComponent<MovementComponent>();

                if (velocity.velocity.magnitude <= 0) return;

                velocity.velocity -= velocity.velocity * deltaTime;
            }
        }
    }
}