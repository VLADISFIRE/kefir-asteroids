using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public struct MovementComponent : IComponent
    {
        public Vector2 velocity;
        /// <summary>
        /// in rad
        /// </summary>
        public float angleVelocity;
    }
}