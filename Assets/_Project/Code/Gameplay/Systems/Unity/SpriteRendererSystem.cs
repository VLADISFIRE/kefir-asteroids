using System.Collections.Generic;
using Gameplay.Utility;
using Infrastructure.ECS;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Render
{
    public class SpriteRendererSystem : BaseSystem
    {
        private Mask _mask;

        private Dictionary<Entity, SpriteRenderer> _renderers = new(32);

        private SpriteRendererPool _pool;

        public SpriteRendererSystem(SpriteRendererPool pool)
        {
            _pool = pool;
        }

        protected override void OnPlayed()
        {
            foreach (var renderer in _renderers.Values)
            {
                _pool.Release(renderer);
            }

            _renderers.Clear();
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent, SpriteRendererComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            using (ListPool<Entity>.Get(out var list))
            {
                foreach (var pair in _renderers)
                {
                    if (pair.Key.IsAlive()) continue;

                    _pool.Release(pair.Value);
                    list.Add(pair.Key);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    _renderers.Remove(list[i]);
                }
            }

            foreach (var entity in _mask)
            {
                ref var render = ref entity.GetComponent<SpriteRendererComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                if (!_renderers.TryGetValue(entity, out var view))
                {
                    view = _pool.Get();
                    _renderers.Add(entity, view);

                    view.sprite = render.sprite;

                    if (render.color != Color.clear)
                        view.color = render.color;

                    view.sortingOrder = render.layer;

                    if (render.size != Vector2.zero)
                    {
                        view.drawMode = SpriteDrawMode.Sliced;
                        view.size = render.size;
                    }
                    else
                    {
                        view.drawMode = SpriteDrawMode.Simple;
                    }

                    view.transform.localScale = Vector3.one;
                }
                else
                {
                    view.sprite = render.sprite;
                }

                view.transform.position = transform.position;
                view.transform.rotation = transform.rotation.ToQuaternion();
            }
        }
    }
}