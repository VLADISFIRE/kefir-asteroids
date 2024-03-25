using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public struct ParticleEvent : IEvent
    {
        public Vector2 position;
    }
}