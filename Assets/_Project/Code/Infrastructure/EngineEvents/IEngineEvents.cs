using System;

namespace Infrastructure
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
        public float GetDeltaTime(EventType type);

        public void Subscribe(EventType type, Action action);
        public void Unsubscribe(EventType type, Action action);
    }
}