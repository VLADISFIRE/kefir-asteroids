namespace Infrastructure.ECS
{
    public interface IEventReactionSystem<T> : ISystem
    {
        public void ReactOn(T command);
    }
}