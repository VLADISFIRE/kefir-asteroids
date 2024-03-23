namespace Infrastructure.ECS
{
    public struct EntityBeforeDestroyedEvent
    {
        public Entity entity;
    }

    public struct EntityDestroyedCommand
    {
        public int index;
    }
}