namespace Infrastructure.ECS
{
    public sealed partial class Mask
    {
        public Entity this[int index]
        {
            get
            {
                var id = _ids[index];
                return _world[id];
            }
        }

        private bool Check(int id)
        {
            return _world.Check(id, _include, _exclude);
        }
    }
}