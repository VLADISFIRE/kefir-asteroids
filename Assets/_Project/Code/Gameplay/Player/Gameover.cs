using System;

namespace Gameplay
{
    public class Gameover
    {
        private bool _enable;

        public bool enable { get { return _enable; } }

        public event Action<bool> updated;

        public void Set(bool value)
        {
            _enable = value;
            updated?.Invoke(value);
        }
    }
}