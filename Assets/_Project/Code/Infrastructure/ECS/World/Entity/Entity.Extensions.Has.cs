namespace Infrastructure.ECS
{
    public static class EntityExtensionsHas
    {
        public static bool HasComponent<T>(this Entity entity) 
            where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return world.Has<T>(entity.index);
        }

        public static bool HasComponent<T, T1>(this Entity entity)
            where T : struct, IComponent
            where T1 : struct, IComponent
        {
            return
                HasComponent<T>(entity) &&
                HasComponent<T1>(entity);
        }

        public static bool HasComponent<T, T1, T2>(this Entity entity)
            where T : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            return
                HasComponent<T>(entity) &&
                HasComponent<T1>(entity) &&
                HasComponent<T2>(entity);
        }

        public static bool HasComponent<T, T1, T2, T3>(this Entity entity)
            where T : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            return
                HasComponent<T>(entity) &&
                HasComponent<T1>(entity) &&
                HasComponent<T3>(entity) &&
                HasComponent<T2>(entity);
        }
    }
}