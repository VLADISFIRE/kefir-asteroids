using System.Text;

namespace Utility.Pooling
{
    public static class StringBuilderPool
    {
        private static ObjectPool<StringBuilder> _instance = new ObjectPool<StringBuilder>(new PoolPolicy());

        public static StringBuilder Get()
        {
            return _instance.Get();
        }

        public static PooledObject<StringBuilder> Get(out StringBuilder result)
        {
            return _instance.Get(out result);
        }
        
        public static void Release(StringBuilder stringBuilder)
        {
            _instance.Release(stringBuilder);
        }

        class PoolPolicy : DefaultObjectPoolPolicy<StringBuilder>
        {
            public override void OnReturn(StringBuilder obj)
            {
                obj.Clear();
            }
        }
    }
}