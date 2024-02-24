namespace Infrastructure.ECS
{
    public sealed partial class Mask
    {
        public partial struct Enumerator
        {
            private int _increment;

            private readonly Mask _mask;

            internal Enumerator(Mask mask)
            {
                _mask = mask;

                _increment = -1;
            }

            public bool MoveNext()
            {
                return ++_increment < _mask.count;
            }
        }
    }
}