using Infrastructure.DI;
using Infrastructure.ECS;

namespace Initializers.Ecs
{
    public class EcsInitializer
    {
        public EcsInitializer(Scope scope)
        {
            scope.Register<IEcsSystemFactory, EcsSystemFactory>();
            scope.Register<IEcsManager, EcsManager>();
        }
    }
}