using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class CollisionSystem : BaseEventSystem<CollisionEvent>
    {
        private Mask _mask;
        private ICollisionMatrix _matrix;

        public CollisionSystem(ICollisionMatrix matrix)
        {
            _matrix = matrix;
        }

        protected override void OnInitialized()
        {
            Mask<ColliderComponent, TransformComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            var matrix = GetRawCheckMatrix(_mask.count);

            var x = 0;
            foreach (var entity in _mask)
            {
                var y = -1;

                ref var transform = ref entity.GetComponent<TransformComponent>();
                ref var collision = ref entity.GetComponent<ColliderComponent>();

                foreach (var entity2 in _mask)
                {
                    y++;

                    if (matrix[x, y]) continue;

                    matrix[x, y] = true;
                    matrix[y, x] = true;

                    ref var transform2 = ref entity2.GetComponent<TransformComponent>();
                    ref var collision2 = ref entity2.GetComponent<ColliderComponent>();

                    if (Intersect(ref transform.position, ref collision,
                            ref transform2.position, ref collision2))
                    {
                        entity.SetComponent(new CollisionEvent
                        {
                            entity = entity2
                        });

                        entity2.SetComponent(new CollisionEvent
                        {
                            entity = entity
                        });
                    }
                }

                x++;
            }
        }

        private bool Intersect(
            ref Vector2 posA, ref ColliderComponent a,
            ref Vector2 posB, ref ColliderComponent b)
        {
            if (!Collision(a.layer, b.layer))
                return false;

            var distance = (posA - posB).magnitude;
            var allowedDistance = a.radius + b.radius;

            return distance < allowedDistance;
        }

        private bool Collision(int layerA, int layerB)
        {
            return _matrix.Check(layerA, layerB);
        }

        private bool[,] GetRawCheckMatrix(int size)
        {
            var matrix = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                matrix[i, i] = true;
            }

            return matrix;
        }
    }
}