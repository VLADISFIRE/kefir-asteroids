using System;
using Gameplay;
using Infrastructure.ECS;

namespace UI
{
    public class GameoverWindow : UIElement, IDisposable
    {
        private GameoverWindowLayout _layout;

        private IEcsManager _manager;
        private PlayerScore _score;

        public GameoverWindow(
            GameoverWindowLayout layout,
            IEcsManager manager,
            PlayerScore score) : base(layout.gameObject)
        {
            _score = score;
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
            _layout.score.text = $"Score: {_score.value}";
        }

        private void HandleButtonClicked()
        {
            _manager.Restart();
        }
    }
}