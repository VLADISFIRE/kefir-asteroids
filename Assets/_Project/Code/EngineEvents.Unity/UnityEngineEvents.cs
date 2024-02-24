using System;
using UnityEngine;

namespace Game
{
    public class UnityEngineEvents : MonoBehaviour, IEngineEventsSource
    {
        public float deltaTime { get { return Time.deltaTime; } }
        
        public event Action<EventType> invoked;

        private void Update()
        {
            Invoke(EventType.Update);
        }

        private void FixedUpdate()
        {
            Invoke(EventType.FixedUpdate);
        }

        private void LateUpdate()
        {
            Invoke(EventType.LateUpdate);
        }

        private void OnDestroy()
        {
            Invoke(EventType.Dispose);
        }

        private void Invoke(EventType type)
        {
            invoked?.Invoke(type);
        }
    }
}