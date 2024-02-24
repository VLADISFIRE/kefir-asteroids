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

                float speed = 1;
                if (entity.HasComponent<SpeedComponent>())
                {
                    speed = entity.GetComponent<SpeedComponent>().value;
                }

                transform.position += movement.velocity * speed;
            }
        }
    }
}