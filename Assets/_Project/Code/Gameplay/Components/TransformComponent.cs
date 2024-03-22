using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public struct TransformComponent : IComponent
    {
        public Vector2 position;
        public Vector2 rotation;

        public override string ToString()
        {
            return
                $"Position: {position}\n" +
                $"Rotation: {rotation}";
        }
    }
}