using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public sealed class Pool<T> : IPool, IDisposable
    {
        private T[] _array;

        private int _increment;
        private readonly Stack<int> _freeStack;

        internal ref T this[int index] { get { return ref _array[index]; } }

        internal IReadOnlyCollection<int> free { get { return _freeStack; } }

        public int capacity { get { return _array.Length; } }

        public event Action<int> resized;

        internal Pool(int capacity = 128)
        {
            _array = new T[capacity];
            _freeStack = new Stack<int>(capacity);
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
        }

        public ref T Get(out int index, T value = default)
        {
            index = Get(value);

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