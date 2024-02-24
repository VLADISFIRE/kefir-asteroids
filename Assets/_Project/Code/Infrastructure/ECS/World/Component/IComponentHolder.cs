using System;

namespace Infrastructure.ECS
{
    public interface IComponentHolder : IDisposable
    {
        public event Action<int, int> added;
        public event Action<int, int> removed;

        public bool Remove(int id);
        public bool Has(int id);

        public void Resize(int capacity);
        public bool Clear(int id);
    }
}