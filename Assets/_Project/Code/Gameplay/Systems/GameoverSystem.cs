using System;
using Infrastructure.ECS;

namespace Gameplay
{
    public class GameoverSystem : BaseSystem
    {
        private Mask _mask;

        private bool _gameover;

        public event Action<bool> changed;
        public bool gameover { get { return _gameover; } }

        protected override void OnPlayed()
        {
            SetGameover(false);
        }

        protected override void OnInitialize()
        {
            Mask<RocketTag, DestroyEvent>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var _ in _mask)
            {
                SetGameover(true);
            }
        }

        private void SetGameover(bool value)
        {
            _gameover = value;
            changed?.Invoke(value);
        }
    }
}