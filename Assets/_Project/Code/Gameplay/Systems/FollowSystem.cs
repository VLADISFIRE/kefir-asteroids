using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class FollowSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<FollowComponent, MovementComponent, TransformComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var movement = ref entity.GetComponent<MovementComponent>();

                //Clear on case empty target
                movement.velocity = Vector2.zero;

                ref var follow = ref entity.GetComponent<FollowComponent>();

                var target = follow.target;

                if (!target.IsAlive())
                {
                    follow.target = Entity.empty;
                    continue;
                }

                if (target.HasComponent<TransformComponent>())
                {
                    ref var targetTransform = ref target.GetComponent<TransformComponent>();

                    ref var transform = ref entity.GetComponent<TransformComponent>();

                    var offset = targetTransform.position - transform.position;
                    var direction = offset.normalized;

                    movement.velocity = direction * follow.speed;
                }
            }
        }
    }
}