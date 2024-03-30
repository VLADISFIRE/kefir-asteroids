using Infrastructure.ECS;

namespace Gameplay
{
    public class GameoverSystem : BaseSystem
    {
        private Mask _mask;
        private Gameover _gameover;

        public GameoverSystem(Gameover gameover)
        {
            _gameover = gameover;
        }

        protected override void OnPlayed()
        {
            _gameover.Set(false);
        }

        protected override void OnInitialize()
        {
            Mask<RocketTag>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            _gameover.Set(_mask.count <= 0);
        }
    }
}