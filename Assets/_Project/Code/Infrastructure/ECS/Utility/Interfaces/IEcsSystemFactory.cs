namespace Infrastructure.ECS
{
    public interface IEcsSystemFactory
    {
        public T Create<T>() where T : BaseSystem;
    }
}