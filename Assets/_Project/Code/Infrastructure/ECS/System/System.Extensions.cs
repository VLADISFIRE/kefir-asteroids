namespace Infrastructure.ECS
{
    public static class SystemExtensions
    {
        public static T AddSystem<T>(this Systems systems, T system) where T : BaseSystem
        {
            return systems.Add<T>(system);
        }

        public static void RemoveSystem<T>(this Systems systems) where T : BaseSystem
        {
            systems.Remove<T>();
        }

        public static void Initialize(this Systems systems)
        {
            systems.Initialize();
        }

        public static void Update(this Systems systems, float deltaTime)
        {
            systems.Update(deltaTime);

            systems.LateUpdate();
        }
    }
}