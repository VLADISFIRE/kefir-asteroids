namespace Infrastructure.ECS
{
    public sealed partial class Mask
    {
        public partial struct Enumerator
        {
            public Entity Current { get { return _mask[_i]; } }
        }
    }
}