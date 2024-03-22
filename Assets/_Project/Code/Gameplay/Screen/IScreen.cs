using UnityEngine;

namespace Gameplay
{
    public interface IScreen
    {
        Vector2 lowerLeft { get; }
        Vector2 topRight { get; }
    }
}