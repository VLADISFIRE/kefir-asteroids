using System;

namespace Game
{
    public interface IEngineEventsSource
    {
        public float deltaTime { get; }
        
        public event Action<EventType> invoked;
    }
}