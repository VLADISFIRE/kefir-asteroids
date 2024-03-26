using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public struct ColliderComponent : IComponent
    {
        public ColliderType type;
        
        public float radius;

        //Line
        public float size;
        public Vector2 direction;

        public int layer;
    }

    public enum ColliderType
    {
        Circle,
        Line
    }
}