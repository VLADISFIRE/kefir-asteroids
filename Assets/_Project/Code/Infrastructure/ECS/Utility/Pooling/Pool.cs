using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public sealed class Pool<T> : IPool<T>, IDisposable
    {
        private const int DEFAULT_CAPACITY = 128;

        private T[] _array;

        private int _increment;
        private readonly Stack<int> _freeStack;
        private IPoolPolicy<T> _policy;

        internal ref T this[int index] { get { return ref _array[index]; } }

        internal IReadOnlyCollection<int> free { get { return _freeStack; } }

        public int capacity { get { return _array.Length; } }

        public event Action<int> resized;

        internal Pool(int capacity = DEFAULT_CAPACITY)
        {
            _array = new T[capacity];
            _freeStack = new Stack<int>(capacity);
        }

        internal Pool(IPoolPolicy<T> policy, int capacity = DEFAULT_CAPACITY) : this(capacity)
        {
            _policy = policy;
        }

        internal void Set(int index, T value = default)
        {
            _array[index] = value;
        }

        public void Dispose()
        {
            _array = null;
            _freeStack.Clear();
        }

        public void Release(int index)
        {
            _freeStack.Push(index);

            _policy?.OnRelease(ref this[index]);
        }

        public ref T Get(out int index, T value = default)
        {
            index = Get(value);

            _policy?.OnGet(ref this[index]);

            return ref this[index];
        }

        private int Get(T value = default)
        {
            if (_freeStack.TryPop(out var index))
            {
                Set(index, value);
                return index;
            }

            TryResize();

            index = _increment;
            Set(_increment, value);
            _increment++;

            return index;
        }

        public ref T Get(int index)
        {
            return ref this[index];
        }

        private void TryResize()
        {
            var length = _array.Length;
            if (_increment >= length)
            {
                var newCapacity = length * 2;
                Array.Resize(ref _array, newCapacity);
                resized?.Invoke(newCapacity);
            }
        }
    }
}