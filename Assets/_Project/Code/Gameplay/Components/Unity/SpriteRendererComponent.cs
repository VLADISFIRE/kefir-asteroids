using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public struct SpriteRendererComponent : IComponent
    {
        public Sprite sprite;
        public Color color;
        public int layer;
        public Vector2 size;
    }
}