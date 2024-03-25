using Infrastructure.ECS;

namespace Gameplay
{
    public class DestroyScreenOutsidersSystem : BaseSystem
    {
        private Mask _mask;
        private IScreen _screen;

        public DestroyScreenOutsidersSystem(IScreen screen)
        {
            _screen = screen;
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent>().Exclude<PortalTag>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();

                if (!_screen.Contains(transform.position))
                {
                    entity.AddComponent<DestroyEvent>();
                }
            }
        }
    }
}