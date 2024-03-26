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

        public static void Remove(this Entity entity)
        {
            entity.world.TryGetTarget(out var world);

            world.RemoveEntity(entity.index);
        }

        public static bool IsAlive(this Entity entity)
        {
            if (entity.world == null) return false;

            entity.world.TryGetTarget(out var world);

            return world.IsAlive(entity.index);
        }
    }
}