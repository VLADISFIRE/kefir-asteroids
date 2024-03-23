using System.Collections.Generic;
using Gameplay.Utility;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay.Render
{
    public class SpriteRendererSystem : BaseSystem, IEventReactionSystem<EntityBeforeDestroyedEvent>
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

        public void ReactOn(EntityBeforeDestroyedEvent @event)
        {
            ref var entity = ref @event.entity;

            if (_views.TryGetValue(entity, out var view))
            {
                _pool.Release(view);
            }
        }
    }
}