namespace Infrastructure.ECS
{
    public interface ISystemFactory
    {
        public T Create<T>() where T : ISystem;
    }
}