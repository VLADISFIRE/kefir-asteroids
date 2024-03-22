using Infrastructure.ECS;

namespace Gameplay
{
    public class PhysicsSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<RigidbodyComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var rigidbody = ref entity.GetComponent<RigidbodyComponent>();
                ref var movement = ref entity.AddComponent<MovementComponent>();
                
                movement.velocity += rigidbody.acceleration * deltaTime;
            }
        }
    }
}