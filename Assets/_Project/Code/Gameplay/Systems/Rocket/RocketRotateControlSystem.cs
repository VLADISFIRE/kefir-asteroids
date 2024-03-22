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
            Mask<RocketRotateControlComponent, MovementComponent, TransformComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var rotate = ref entity.GetComponent<RocketRotateControlComponent>();
                ref var movement = ref entity.GetComponent<MovementComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                if (!rotate.enable) continue;
                
                var rotateSpeed = rotate.speed * deltaTime;
                movement.angleVelocity += rotate.left ? rotateSpeed : -rotateSpeed;
            }
        }
    }
}