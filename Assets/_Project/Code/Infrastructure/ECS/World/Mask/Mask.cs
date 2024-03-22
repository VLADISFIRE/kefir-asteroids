using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public sealed partial class Mask
    {
        private const int DEFAULT_ENTITY_CAPACITY = 32;

        private List<int> _ids = new(DEFAULT_ENTITY_CAPACITY);

        private HashSet<int> _include;
        private HashSet<int> _exclude;

        public int count { get { return _ids.Count; } }
        
        internal IReadOnlyCollection<int> include { get { return _include; } }
        internal IReadOnlyCollection<int> exclude { get { return _exclude; } }

        internal Mask(HashSet<int> include, HashSet<int> exclude)
        {
            _include = include;
            _exclude = exclude;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        internal void OnComponentAdded(int id, int hash)
        {
            if (!Has(hash)) return;
            
            if (_ids.Contains(id))
            {
                if (!_exclude.Contains(hash)) return;

                Remove(id);
            }
            else
            {
                if (!Check(id)) return;

                Add(id);
            }
        }

        internal void OnComponentRemoved(int id, int hash)
        {
            if (!Has(hash))
                return;

            if (!_ids.Contains(id))
                return;

            Remove(id);
        }

        internal bool Add(int id)
        {
            _ids.Add(id);
            return true;
        }

        internal bool Remove(int id)
        {
            return _ids.Remove(id);
        }

        private bool Has(int hash)
        {
            return _include.Contains(hash) || _exclude.Contains(hash);
        }
    }
}