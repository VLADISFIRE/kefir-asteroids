using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketWeaponSystem : BaseEventSystem<RocketWeaponFireEvent>
    {
        private GameSettings _settings;

        private Mask _fireMask;
        private Mask _swapMask;

        public RocketWeaponSystem(GameSettings settings)
        {
            _settings = settings;
        }

        protected override void OnInitialized()
        {
            Mask<TransformComponent, RocketWeaponComponent, RocketFireEvent>().Build(out _fireMask);
            Mask<RocketWeaponComponent, RocketSwapWeaponEvent>().Build(out _swapMask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _fireMask)
            {
                ref var weapon = ref entity.GetComponent<RocketWeaponComponent>();
                ref var transform = ref entity.GetComponent<TransformComponent>();

                entity.SetComponent(new RocketWeaponFireEvent
                {
                    type = weapon.type
                });
            }
        }
    }
}