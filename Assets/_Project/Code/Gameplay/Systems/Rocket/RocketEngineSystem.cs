using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketEngineSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<RocketEngineComponent, TransformComponent, RigidbodyComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var rocketEngine = ref entity.GetComponent<RocketEngineComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();
                ref var rigidbody = ref entity.GetComponent<RigidbodyComponent>();

                var direction = transform.rotation;
                var force = rocketEngine.enable ? rocketEngine.power : 0;

                rigidbody.acceleration = direction * force * deltaTime;
            }
        }
    }
}