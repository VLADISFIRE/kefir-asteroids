using UnityEngine;
using Utility.Pooling;

namespace Gameplay
{
    public class SpriteRendererPool : ObjectPool<SpriteRenderer>
    {
        public SpriteRendererPool(int capacity) : base(new Policy(), true, capacity: capacity)
        {
        }

        public class Policy : DefaultObjectPoolPolicy<SpriteRenderer>
        {
            private const string NAME = "[ " + nameof(SpriteRendererPool) + " ] {0}";

            private int _count = -1;

            public override SpriteRenderer Create()
            {
                _count++;
                return new GameObject(string.Format(NAME, _count)).AddComponent<SpriteRenderer>();
            }

            public override void OnGet(SpriteRenderer obj)
            {
                obj.gameObject.SetActive(true);
            }

            public override void OnReturn(SpriteRenderer obj)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}