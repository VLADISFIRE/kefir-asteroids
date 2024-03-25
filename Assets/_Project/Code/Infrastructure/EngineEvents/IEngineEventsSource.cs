using System;

namespace Infrastructure
{
    public interface IEngineEventsSource
    {
        public event Action<EventType> invoked;
        public float GetDeltaTime(EventType type);
    }
}