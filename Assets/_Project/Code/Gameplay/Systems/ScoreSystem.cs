using Infrastructure.ECS;

namespace Gameplay
{
    public class ScoreSystem : BaseSystem
    {
        private PlayerScore _playerScore;

        private Mask _mask;

        public ScoreSystem(PlayerScore playerScore)
        {
            _playerScore = playerScore;
        }

        protected override void OnInitialize()
        {
            Mask<DestroyEvent, ScoreComponent>().Build(out _mask);
        }

        protected override void OnPlayed()
        {
            _playerScore.Set(0);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var destroy = ref entity.GetComponent<DestroyEvent>();

                if (destroy.auto) continue;

                ref var score = ref entity.GetComponent<ScoreComponent>();
                _playerScore.Add(score.value);
            }
        }
    }
}