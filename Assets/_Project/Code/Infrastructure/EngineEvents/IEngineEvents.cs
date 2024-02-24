using System;

namespace Game
{
    public enum EventType
    {
        Update,
        FixedUpdate,
        LateUpdate,
        Dispose
    }

    public interface IEngineEvents
    {
        public float deltaTime { get; }

        public void Subscribe(EventType type, Action action);
        public void Unsubscribe(EventType type, Action action);
    }
}