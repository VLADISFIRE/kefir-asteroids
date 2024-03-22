namespace Infrastructure.ECS
{
    public interface IEcsManager
    {
        public IEcsManager AddSystem<T>() where T : BaseSystem;
        public IEcsManager RemoveSystem<T>() where T : BaseSystem;
    }
}