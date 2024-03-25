using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketRotateControlSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<RocketRotateControlComponent, MovementComponent, TransformComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var rotate = ref entity.GetComponent<RocketRotateControlComponent>();
                ref var movement = ref entity.GetComponent<MovementComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                if (!rotate.enable) continue;

                var angleSpeed = rotate.speed * deltaTime;
                movement.angleVelocity += rotate.left ? -angleSpeed : angleSpeed;
            }
        }
    }
}