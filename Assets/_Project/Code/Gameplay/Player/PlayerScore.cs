using System;

namespace Gameplay
{
    public class PlayerScore
    {
        private int _value;

        public int value { get { return _value; } }

        public event Action<int> updated;

        public void Add(int value)
        {
            Set(_value + value);
        }

        public void Set(int value)
        {
            _value = value;
            updated?.Invoke(value);
        }
    }
}