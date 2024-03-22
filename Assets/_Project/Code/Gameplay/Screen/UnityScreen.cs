using UnityEngine;

namespace Gameplay
{
    public class UnityScreen : IScreen
    {
        private const int Z_CAMERA_OFFSET = 1;

        private Camera _camera;
        
        private readonly Vector3 _topRight;
        private readonly Vector3 _lowerLeft;

        public Vector2 lowerLeft { get { return _lowerLeft; } }
        public Vector2 topRight { get { return _topRight; } }

        public UnityScreen(Camera camera)
        {
            _camera = camera;

            var lowerLeftScreenPoint = new Vector3(0, 0, Z_CAMERA_OFFSET);
            var topRightScreenPoint = new Vector3(Screen.width, Screen.height, Z_CAMERA_OFFSET);

            _lowerLeft = _camera.ScreenToWorldPoint(lowerLeftScreenPoint);
            _topRight = _camera.ScreenToWorldPoint(topRightScreenPoint);
        }
    }
}