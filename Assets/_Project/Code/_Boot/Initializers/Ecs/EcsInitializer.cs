using Infrastructure.DI;
using Infrastructure.ECS;

namespace Game
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