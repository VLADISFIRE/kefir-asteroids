namespace Infrastructure.ECS
{
    public interface IEventHandlerSystem : ISystem
    {
    }

    public interface IEventHandlerSystem<T> : IEventHandlerSystem where T : struct
    {
        public void OnReact(T data);
    }
}