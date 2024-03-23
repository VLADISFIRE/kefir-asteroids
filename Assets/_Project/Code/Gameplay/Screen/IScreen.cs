using UnityEngine;

namespace Gameplay
{
    public interface IScreen
    {
        public Vector2 lowerLeft { get; }
        public Vector2 topRight { get; }
        public bool Contains(Vector2 point);
    }
}