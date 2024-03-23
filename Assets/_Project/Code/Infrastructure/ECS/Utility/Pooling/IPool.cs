namespace Infrastructure.ECS
{
    internal interface IPool<T>
    {
        public ref T Get(out int index, T value = default);
        public void Release(int index);
    }
}