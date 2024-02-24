using Infrastructure.DI;
using Infrastructure.ECS;

namespace Game
{
    public class EcsSystemFactory : IEcsSystemFactory
    {
        private Scope _scope;

        public EcsSystemFactory(Scope scope)
        {
            _scope = scope;
        }

        public T Create<T>() where T : BaseSystem
        {
            return _scope.Register<T>();
        }
    }
}