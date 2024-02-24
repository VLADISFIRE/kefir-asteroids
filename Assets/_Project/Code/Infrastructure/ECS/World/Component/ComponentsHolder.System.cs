namespace Infrastructure.ECS
{
    public abstract partial class BaseSystem
    {
        public ComponentsHolder<T> GetComponentsHolder<T>() where T : struct, IComponent
        {
            return _world.GetHolder<T>();
        }

        public void GetComponentsHolder<T>(out ComponentsHolder<T> holder) where T : struct, IComponent
        {
            holder = _world.GetHolder<T>();
        }
    }
}