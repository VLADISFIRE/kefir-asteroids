using System;

namespace Utility.Pooling
{
    public class DefaultObjectPoolPolicy<T> : IObjectPoolPolicy<T> where T : new()
    {
        public virtual T Create()
        {
            return new T();
        }

        public virtual void OnGet(T obj)
        {
        }

        public virtual void OnReturn(T obj)
        {
        }

        public virtual void OnDispose(T obj)
        {
            if (obj is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}