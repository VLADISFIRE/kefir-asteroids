using Gameplay.Utility;
using Infrastructure.ECS;
using Object = UnityEngine.Object;

namespace Gameplay.Render
{
    public class AsteroidRenderSystem : BaseSystem
    {
        private Mask _mask;
        private GameSettingsScrobject _settings;

        private PlayerView _playerView;

        public AsteroidRenderSystem(GameSettingsScrobject settings)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            Mask<TransformComponent, AsteroidTag>().Build(out _mask);

            _playerView = Object.Instantiate(_settings.player.prefab);
            _playerView.SetEnable(false);
        }

        protected override void OnDispose()
        {
            if (_playerView != null)
                Object.Destroy(_playerView.gameObject);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();

                _playerView.SetEnable(true);
                _playerView.transform.position = transform.position;
                _playerView.transform.rotation = transform.rotation.ToQuaternion();
            }
        }
    }
}