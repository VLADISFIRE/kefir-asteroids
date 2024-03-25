using System;
using Infrastructure;
using UnityEngine;
using EventType = Infrastructure.EventType;

public class UnityEngineEvents : MonoBehaviour, IEngineEventsSource
{
    public float deltaTime { get { return Time.deltaTime; } }

    public event Action<EventType> invoked;

    public float GetDeltaTime(EventType type)
    {
        switch (type)
        {
            case EventType.Update:
                return Time.deltaTime;
            case EventType.FixedUpdate:
                return Time.fixedDeltaTime;
        }

        return 0;
    }

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