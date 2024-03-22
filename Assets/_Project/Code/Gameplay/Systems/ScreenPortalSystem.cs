using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class ScreenPortalSystem : BaseSystem
    {
        private const int Z_CAMERA_OFFSET = 1;
        
        private Camera _camera;

        private Mask _mask;

        public ScreenPortalSystem(Camera camera)
        {
            _camera = camera;
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();
                
                var topRightScreenPoint = new Vector3(Screen.width, Screen.height,Z_CAMERA_OFFSET);
                var lowerLeftScreenPoint = new Vector3(0, 0,Z_CAMERA_OFFSET);

                var topRight = _camera.ScreenToWorldPoint(topRightScreenPoint);
                var lowerLeft = _camera.ScreenToWorldPoint(lowerLeftScreenPoint);
                
                //Teleport by x
                if (transform.position.x < lowerLeft.x)
                    transform.position.x = topRight.x;
                else if (transform.position.x > topRight.x)
                    transform.position.x = lowerLeft.x;
                
                //Teleport by y
                if (transform.position.y < lowerLeft.y)
                    transform.position.y = topRight.y;
                else if (transform.position.y > topRight.y)
                    transform.position.y = lowerLeft.y;
            }
        }
    }
}