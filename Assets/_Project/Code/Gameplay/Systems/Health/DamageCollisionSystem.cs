using Infrastructure.ECS;

namespace Gameplay
{
    public class DamageCollisionSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<CollisionEvent, DamageCollisionComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var collision = ref entity.GetComponent<CollisionEvent>();
                ref var damageCollision = ref entity.GetComponent<DamageCollisionComponent>();

                if (!collision.entity.IsAlive()) continue;

                collision.entity.SetComponent(new DamageEvent
                {
                    value = damageCollision.value,
                });

                if (!damageCollision.autoDestroy) continue;

                entity.AddComponent<DestroyEvent>();
            }
        }
    }
}