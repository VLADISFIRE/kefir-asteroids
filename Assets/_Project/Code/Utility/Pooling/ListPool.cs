using System.Collections.Generic;

namespace Utility.Pooling
{
    public static class ListPool<T>
    {
        private static readonly ObjectPool<List<T>> _instance = new ObjectPool<List<T>>(new ListPoolPolicy<T>(), true);

        public static List<T> Get()
        {
            return _instance.Get();
        }

        public static PooledObject<List<T>> Get(out List<T> result)
        {
            return _instance.Get(out result);
        }

        public static PooledObject<List<T>> GetCopy(IEnumerable<T> source, out List<T> result)
        {
            var disposable = _instance.Get(out result);

            if (source != null)
            {
                result.AddRange(source);
            }

            return disposable;
        }

        public static void Release(List<T> list)
        {
            _instance.Release(list);
        }
    }

    public class ListPoolPolicy<T> : DefaultObjectPoolPolicy<List<T>>
    {
        public override void OnReturn(List<T> list)
        {
            list.Clear();
        }
    }
}