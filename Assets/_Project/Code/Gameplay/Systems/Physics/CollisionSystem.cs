using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class CollisionSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<CollisionComponent, TransformComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var collider = ref entity.GetComponent<CollisionComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                foreach (var entity2 in _mask)
                {
                    if (entity == entity2) continue;

                    if (entity.HasComponent<AsteroidTag>() && entity2.HasComponent<AsteroidTag>()) continue;

                    ref var collider2 = ref entity2.GetComponent<CollisionComponent>();
                    ref var transform2 = ref entity2.GetComponent<TransformComponent>();

                    var distance = (transform2.position - transform.position).magnitude;

                    var allowedDistance = collider.radius + collider2.radius;
                    if (distance > allowedDistance) continue;

                    Debug.LogError($"{entity} collision {entity2}");
                }
            }
        }
    }
}