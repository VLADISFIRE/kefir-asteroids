using System;
using Gameplay;
using Infrastructure.ECS;

namespace UI
{
    public class GameoverWindow : UIElement, IDisposable
    {
        private GameoverWindowLayout _layout;

        private IEcsManager _manager;
        private ScoreSystem _scoreSystem;

        public GameoverWindow(GameoverWindowLayout layout, IEcsManager manager, ScoreSystem scoreSystem) : base(layout.gameObject)
        {
            _scoreSystem = scoreSystem;
            _manager = manager;
            _layout = layout;

            _layout.button.onClick.AddListener(HandleButtonClicked);
        }

        public void Dispose()
        {
            _layout.button.onClick.RemoveListener(HandleButtonClicked);
        }

        protected override void OnShow()
        {
            _layout.score.text = $"Score: {_scoreSystem.score}";
        }

        private void HandleButtonClicked()
        {
            _manager.Restart();
        }
    }
}