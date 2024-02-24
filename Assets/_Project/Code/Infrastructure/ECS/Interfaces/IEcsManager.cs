namespace Infrastructure.ECS
{
    public interface IEcsManager
    {
        public void AddSystem<T>() where T : BaseSystem;
        public void RemoveSystem<T>() where T : BaseSystem;
    }
}