using Infrastructure.DI;
using Infrastructure.ECS;

namespace Initializers.Ecs
{
    public class EcsInitializer
    {
        public EcsInitializer(Scope scope)
        {
            scope.Register<ISystemFactory, SystemFactory>();
            scope.Register<IEcsManager, EcsManager>().SetDefaultType(EngineType.Fixed);
        }
    }
}