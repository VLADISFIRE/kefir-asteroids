﻿using Infrastructure.ECS;

namespace Gameplay
{
    public struct DestroyEvent : IEvent
    {
        public bool auto;
    }
}