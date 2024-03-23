namespace Infrastructure.ECS
{
    public static class EntityExtensionsAdd
    {
        public static ref T AddComponent<T>(this Entity entity)
            where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return ref world.Add<T>(entity.index);
        }

        public static ref T AddComponent<T>(this Entity entity, out bool exist)
            where T : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);

            return ref world.Add<T>(entity.index, out exist);
        }

        public static void AddComponent<T, T1>(this Entity entity)
            where T : struct, IComponent
            where T1 : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);
            world.Add<T>(entity.index);
            world.Add<T1>(entity.index);
        }

        public static void AddComponent<T, T1, T2>(this Entity entity)
            where T : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);
            world.Add<T>(entity.index);
            world.Add<T1>(entity.index);
            world.Add<T2>(entity.index);
        }

        public static void AddComponent<T, T1, T2, T3>(this Entity entity)
            where T : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            entity.world.TryGetTarget(out var world);
            world.Add<T>(entity.index);
            world.Add<T1>(entity.index);
            world.Add<T3>(entity.index);
        }
    }
}