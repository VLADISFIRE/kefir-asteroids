using Game;
using Infrastructure.DI;
using UnityEngine;

public class EngineEventsInitializer
{
    public EngineEventsInitializer(Scope scope)
    {
        var engineEventsSource = CreateEngineEventsSource();
        scope.RegisterInstance(engineEventsSource);

        scope.Register<IEngineEvents, EngineEvents>();

        scope.Register<ScopeDisposer>();
    }

    private static IEngineEventsSource CreateEngineEventsSource()
    {
        return new GameObject("[EngineEvents]").AddComponent<UnityEngineEvents>();
    }
}