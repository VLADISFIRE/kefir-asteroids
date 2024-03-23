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
            var matrix = GetRawCheckMatrix(_mask.count);

            var x = 0;
            foreach (var entity in _mask)
            {
                var y = -1;

                ref var transform = ref entity.GetComponent<TransformComponent>();

                foreach (var entity2 in _mask)
                {
                    y++;

                    if (matrix[x, y]) continue;

                    matrix[x, y] = true;
                    matrix[y, x] = true;

                    ref var transform2 = ref entity2.GetComponent<TransformComponent>();

                    if (Intersect(ref transform.position, ref entity.GetComponent<CollisionComponent>(),
                            ref transform2.position, ref entity2.GetComponent<CollisionComponent>()))
                    {
                        //InvokeE
                    }
                }

                x++;
            }
        }

        private bool Intersect(
            ref Vector2 posA, ref CollisionComponent a,
            ref Vector2 posB, ref CollisionComponent b)
        {
            if (!Collision(a.layer, b.layer))
                return false;

            var distance = (posA - posB).magnitude;
            var allowedDistance = a.radius + b.radius;

            return distance < allowedDistance;
        }

        private bool Collision(int layerA, int layerB)
        {
            if (layerA == CollisionLayer.ASTEROID && layerB == CollisionLayer.ASTEROID) return false;
            
            return true;
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