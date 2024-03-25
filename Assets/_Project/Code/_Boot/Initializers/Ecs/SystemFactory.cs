using Infrastructure.DI;
using Infrastructure.ECS;

namespace Initializers.Ecs
{
    public class SystemFactory : ISystemFactory
    {
        private Scope _scope;

        public SystemFactory(Scope scope)
        {
            _scope = scope;
        }

        public T Create<T>() where T : ISystem
        {
            return _scope.Register<T>();
        }
    }
}