using UnityEngine;
using Utility.Pooling;

namespace Gameplay
{
    public class SpriteRendererPool : ObjectPool<SpriteRenderer>
    {
        // private List<SpriteRenderer> _list;
        //
        // private int _increment;
        //
        // private GameObject _parent;
        // private int _capacity;
        //
        // public SpriteRendererPool(int capacity)
        // {
        //     _capacity = capacity;
        //     _list = new(capacity);
        // }
        //
        // public void Initialize()
        // {
        //     _parent = new GameObject("SpriteRendererPool");
        //     for (int i = 0; i < _capacity; i++)
        //     {
        //         var renderer = Create();
        //         _list.Add(renderer);
        //     }
        // }
        //
        // public void Dispose()
        // {
        //     Object.Destroy(_parent);
        // }
        //
        // public SpriteRenderer Get()
        // {
        //     if (_increment >= _list.Count)
        //     {
        //         var newRenderer = Create();
        //         _list.Add(newRenderer);
        //         _capacity++;
        //     }
        //
        //     var renderer = _list[_increment];
        //     renderer.gameObject.SetActive(true);
        //     _increment++;
        //     return renderer;
        // }
        //
        // public void Release(SpriteRenderer instance)
        // {
        //     _list.Add(instance);
        //     instance.gameObject.SetActive(false);
        //     _increment--;
        // }
        //
        // private SpriteRenderer Create()
        // {
        //     var gameObject = new GameObject($"SpriteRenderer:{_capacity}");
        //     gameObject.transform.SetParent(_parent.transform);
        //     var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        //     gameObject.SetActive(false);
        //     return spriteRenderer;
        // }
        public SpriteRendererPool() : base(new Policy(), true)
        {
        }

        public class Policy : DefaultObjectPoolPolicy<SpriteRenderer>
        {
            public override SpriteRenderer Create()
            {
                return new GameObject().AddComponent<SpriteRenderer>();
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