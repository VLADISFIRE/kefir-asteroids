namespace Infrastructure.ECS
{
    public partial struct Entity
    {
        public static Entity empty = new(-1);
        
        public int index { get; internal set; }

        public Entity(int index)
        {
            this.index = index;
            world = null;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            return a.index == b.index && a.index == b.index;
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public int CompareTo(Entity other)
        {
            return index - other.index;
        }

        public override int GetHashCode()
        {
            return index;
        }

        public bool Equals(Entity entity)
        {
            return entity.index == index;
        }

        internal bool IsAlive()
        {
            return index >= 0;
        }

        public override string ToString()
        {
            return IsAlive() ? $"Entity [ {index} ]" : $"Entity [ null ]";
        }
    }
}