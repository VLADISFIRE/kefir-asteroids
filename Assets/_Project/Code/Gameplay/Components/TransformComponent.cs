using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public struct TransformComponent : IComponent
    {
        public Vector2 position;
        public Vector2 scale;
        public float rotation;
    }
}