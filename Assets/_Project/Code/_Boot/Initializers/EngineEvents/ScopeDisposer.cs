using Infrastructure.DI;

namespace Game
{
    public class ScopeDisposer
    {
        private Scope _scope;
        private IEngineEvents _engineEvents;

        public ScopeDisposer(Scope scope, IEngineEvents engineEvents)
        {
            _scope = scope;
            _engineEvents = engineEvents;

            _engineEvents.Subscribe(EventType.Dispose, Dispose);
        }

        public void Dispose()
        {
            _engineEvents.Unsubscribe(EventType.Dispose, Dispose);

            _scope?.Dispose();
        }
    }
}