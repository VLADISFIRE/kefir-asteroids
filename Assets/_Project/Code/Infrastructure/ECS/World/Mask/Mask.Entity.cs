namespace Infrastructure.ECS
{
    public sealed partial class Mask
    {
        internal Entity this[int index]
        {
            get
            {
                var id = _ids[index];
                return _world[id];
            }
        }

        private bool CheckByEntity(int id)
        {
            var entity = _world[id];

            foreach (var hash in _include)
            {
                if (!_world.Has(entity.index, hash)) return false;
            }

            foreach (var hash in _exclude)
            {
                if (_world.Has(entity.index, hash)) return false;
            }

            return true;
        }
    }
}