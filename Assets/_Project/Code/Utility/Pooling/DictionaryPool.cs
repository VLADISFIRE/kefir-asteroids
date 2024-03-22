using System.Collections.Generic;

namespace Utility.Pooling
{
    public class DictionaryPool<T0, T1>
    {
        private static ObjectPool<Dictionary<T0, T1>> _instance = new ObjectPool<Dictionary<T0, T1>>(new Policy(), true);

        public static Dictionary<T0, T1> Get()
        {
            return _instance.Get();
        }

        public static PooledObject<Dictionary<T0, T1>> Get(out Dictionary<T0, T1> result)
        {
            return _instance.Get(out result);
        }

        public static void Release(Dictionary<T0, T1> dict)
        {
            _instance.Release(dict);
        }

        private class Policy : DefaultObjectPoolPolicy<Dictionary<T0, T1>>
        {
            public override void OnReturn(Dictionary<T0, T1> dict)
            {
                dict.Clear();
            }
        }
    }
}