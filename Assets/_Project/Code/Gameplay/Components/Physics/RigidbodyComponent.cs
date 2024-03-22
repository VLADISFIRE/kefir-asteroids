using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public struct RigidbodyComponent : IComponent
    {
        public Vector2 acceleration;
    }
}