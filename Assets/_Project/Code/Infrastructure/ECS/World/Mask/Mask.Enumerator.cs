namespace Infrastructure.ECS
{
    public sealed partial class Mask
    {
        public partial struct Enumerator
        {
            private int _i;

            private readonly Mask _mask;

            internal Enumerator(Mask mask)
            {
                _mask = mask;

                _i = _mask.count;
            }

            public bool MoveNext()
            {
                return --_i >= 0;
            }
        }
    }
}