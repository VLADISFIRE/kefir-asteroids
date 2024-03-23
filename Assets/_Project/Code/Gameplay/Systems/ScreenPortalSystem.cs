using Infrastructure.ECS;

namespace Gameplay
{
    public class ScreenPortalSystem : BaseSystem
    {
        private IScreen _screen;
        
        private Mask _mask;

        public ScreenPortalSystem(IScreen screen)
        {
            _screen = screen;
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent, PortalTag>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();

                //Teleport by x
                if (transform.position.x < _screen.lowerLeft.x)
                    transform.position.x = _screen.topRight.x;
                else if (transform.position.x > _screen.topRight.x)
                    transform.position.x = _screen.lowerLeft.x;

                //Teleport by y
                if (transform.position.y < _screen.lowerLeft.y)
                    transform.position.y = _screen.topRight.y;
                else if (transform.position.y > _screen.topRight.y)
                    transform.position.y = _screen.lowerLeft.y;
            }
        }
    }
}