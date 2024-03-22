using Infrastructure.DI;
using Initializers.Ecs;
using Initializers.EngineEvents;
using Initializers.Gameplay;
using Initializers.UI;
using UnityEngine;

public class Bootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Boot()
    {
        var scope = new Scope();

        scope.Register<EngineEventsInitializer>();

        scope.Register<GameInput>();

        scope.Register<EcsInitializer>();

        scope.Register<GameplayInitializer>();
        scope.Register<UIInitializer>();
    }
}