using Gameplay.Utility;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class RocketRotateControlSystem : BaseSystem
    {
        private GameSettingsScrobject _settings;

        private Mask _mask;

        public RocketRotateControlSystem(GameSettingsScrobject settings)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            Mask<RocketRotateControlComponent, TransformComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var rotate = ref entity.GetComponent<RocketRotateControlComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                if (!rotate.enable) continue;

                var direction = rotate.left ? transform.rotation.Left() : transform.rotation.Right();

                transform.rotation = Vector2.LerpUnclamped(transform.rotation, direction, deltaTime * rotate.speed);
                transform.rotation.Normalize();
            }
        }
    }
}