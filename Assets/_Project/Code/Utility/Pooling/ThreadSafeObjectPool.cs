using System;

namespace Utility.Pooling
{
    public class ThreadSafeObjectPool<T> : IObjectPool<T>, IDisposable
    {
        private readonly ObjectPool<T> _underlyingPool;

        public ThreadSafeObjectPool(ObjectPool<T> underlyingPool)
        {
            _underlyingPool = underlyingPool;
        }

        public T Get()
        {
            lock (_underlyingPool)
            {
                return _underlyingPool.Get();
            }
        }

        public void Release(T obj)
        {
            lock (_underlyingPool)
            {
                _underlyingPool.Release(obj);
            }
        }

        public void Dispose()
        {
            lock (_underlyingPool)
            {
                _underlyingPool.Dispose();
            }
        }
    }
}