using Game.Gameplay;
using Game.Presenters;
using Game.UI;
using Infrastructure.DI;
using UnityEngine;

namespace Game
{
    public class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Boot()
        {
            var scope = new Scope();

            scope.Register<GameInput>();

            scope.Register<EngineEventsInitializer>();

            scope.Register<EcsInitializer>();

            scope.Register<GameplayInitializer>();
            scope.Register<PresentersInitializer>();
            scope.Register<UIInitializer>();
        }
    }
}