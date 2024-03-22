using System.Collections.Generic;

namespace Utility.Pooling
{
    public class HashSetPool<T>
    {
        private static ObjectPool<HashSet<T>> _instance = new ObjectPool<HashSet<T>>(new Policy(), true);

        public static HashSet<T> Get()
        {
            return _instance.Get();
        }

        public static PooledObject<HashSet<T>> Get(out HashSet<T> result)
        {
            return _instance.Get(out result);
        }

        public static void Release(HashSet<T> set)
        {
            _instance.Release(set);
        }

        private class Policy : DefaultObjectPoolPolicy<HashSet<T>>
        {
            public override void OnReturn(HashSet<T> hashSet)
            {
                hashSet.Clear();
            }
        }
    }
}