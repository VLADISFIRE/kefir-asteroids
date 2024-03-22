using System.Collections.Generic;
using Utility;
using Gameplay.Utility;
using Infrastructure.ECS;
using UnityEngine;
using Utility.Pooling;

namespace Gameplay.Render
{
    public class SpriteRendererSystem : BaseSystem
    {
        private Mask _mask;

        private Dictionary<Entity, SpriteRenderer> _views = new(32);

        private SpriteRendererPool _pool;

        protected override void OnInitialize()
        {
            Mask<TransformComponent, SpriteRendererComponent>().Build(out _mask);

            _pool = new SpriteRendererPool();
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var view = ref entity.GetComponent<SpriteRendererComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                if (!_views.ContainsKey(entity) || _views[entity] == null)
                {
                    var spriteRenderer = _pool.Get();
                    _views[entity] = spriteRenderer;

                    spriteRenderer.sprite = view.sprite;

                    spriteRenderer.gameObject.name = entity.ToString();
                }

                _views[entity].transform.position = transform.position;
                _views[entity].transform.rotation = transform.rotation.ToQuaternion();
            }
        }

        protected override void OnLateUpdate()
        {
            using (ListPool<Entity>.Get(out var list))
            {
                foreach (var pair in _views)
                {
                    var entity = pair.Key;
                    var isAlive = entity.IsAlive();

                    if (isAlive) continue;

                    _pool.Release(pair.Value);

                    if (list == null)
                        list = new List<Entity>(8);

                    list.Add(entity);
                }

                if (list.IsNullOrEmpty()) return;

                for (int i = 0; i < list.Count; i++)
                {
                    _views.Remove(list[i]);
                }
            }
        }
    }
}