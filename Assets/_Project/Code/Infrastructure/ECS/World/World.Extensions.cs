namespace Infrastructure.ECS
{
    public static class WorldExtensions
    {
        public static ref Entity NewEntity(this World world)
        {
            return ref world.NewEntity();
        }

        public static void RemoveEntity(this World world, int index)
        {
            world.RemoveEntity(index);
        }
        
        public static void Clear(this World world)
        {
            world.Clear();
        }
    }
}