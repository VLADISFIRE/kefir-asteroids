namespace Infrastructure.ECS
{
    public static class EntityExtensions
    {
        public static ref T SetComponent<T>(this Entity entity, T component) where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return ref world.Set(entity.index, component);
        }

        public static ref T SetComponent<T>(this Entity entity, T component, out bool exist) where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return ref world.Set(entity.index, component, out exist);
        }

        public static ref T AddComponent<T>(this Entity entity) where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return ref world.Add<T>(entity.index);
        }

        public static ref T AddComponent<T>(this Entity entity, out bool exist) where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return ref world.Add<T>(entity.index, out exist);
        }

        public static ref T GetComponent<T>(this Entity entity) where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return ref world.Get<T>(entity.index);
        }

        public static bool RemoveComponent<T>(this Entity entity) where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return world.Remove<T>(entity.index);
        }

        public static bool HasComponent<T>(this Entity entity) where T : struct, IComponent
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

        public static void Remove(this Entity entity)
        {
            entity.world.TryGetTarget(out var world);

            world.RemoveEntity(entity.index);
        }

        public static bool IsAlive(this Entity entity)
        {
            entity.world.TryGetTarget(out var world);

            return world.IsAlive(entity.index);
        }
    }
}