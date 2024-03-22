using System;

namespace Infrastructure
{
    public interface IEngineEventsSource
    {
        public float deltaTime { get; }
        
        public event Action<EventType> invoked;
    }
}