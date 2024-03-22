using Infrastructure;
using Infrastructure.DI;
using UnityEngine;

namespace Initializers.EngineEvents
{
    public class EngineEventsInitializer
    {
        public EngineEventsInitializer(Scope scope)
        {
            var engineEventsSource = CreateEngineEventsSource();
            scope.RegisterInstance(engineEventsSource);

            scope.Register<IEngineEvents, Infrastructure.EngineEvents>();

            scope.Register<ScopeDisposer>();
        }

        private static IEngineEventsSource CreateEngineEventsSource()
        {
            return new GameObject("[EngineEvents]").AddComponent<UnityEngineEvents>();
        }
    }
}