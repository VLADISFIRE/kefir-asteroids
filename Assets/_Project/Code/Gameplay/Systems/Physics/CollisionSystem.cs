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
            if (!CollisionByMatrix(a.layer, b.layer))
                return false;

            if (b.type == ColliderType.Circle && a.type == ColliderType.Circle)
            {
                var distance = (posA - posB).magnitude;
                var allowedDistance = a.radius + b.radius;

                return distance < allowedDistance;
            }

            if (a.type == ColliderType.Circle && b.type == ColliderType.Line)
            {
                return IntersectLineAndCircle(
                    ref posA, ref a,
                    ref posB, ref b);
            }

            if (a.type == ColliderType.Line && b.type == ColliderType.Circle)
            {
                return IntersectLineAndCircle(
                    ref posB, ref b,
                    ref posA, ref a);
            }

            return false;
        }

        private bool IntersectLineAndCircle(ref Vector2 center, ref ColliderComponent circle, ref Vector2 pos1, ref ColliderComponent line)
        {
            var pos2 = pos1 + line.direction * line.size;
            var radius = circle.radius;

            var dx = pos2.x - pos1.x;
            var dy = pos2.y - pos1.y;

            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (pos1.x - center.x) + dy * (pos1.y - center.y));
            var c = (pos1.x - center.x) * (pos1.x - center.x) + (pos1.y - center.y) * (pos1.y - center.y) -
                radius * radius;

            var d = b * b - 4 * a * c;

            if (d < 0)
                return false;

            var t1 = (-b + Mathf.Sqrt(d)) / (2 * a);
            var t2 = (-b - Mathf.Sqrt(d)) / (2 * a);

            return t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1;
        }

        private bool CollisionByMatrix(int layerA, int layerB)
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