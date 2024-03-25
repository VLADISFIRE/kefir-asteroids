using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketRenderSystem : BaseSystem
    {
        private Mask _mask;
        private GameSettings _settings;

        public RocketRenderSystem(GameSettings settings)
        {
            _settings = settings;
        }

        protected override void OnInitialize()
        {
            Mask<RocketEngineComponent, SpriteRendererComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var rocketEngine = ref entity.GetComponent<RocketEngineComponent>();
                ref var spriteRenderer = ref entity.GetComponent<SpriteRendererComponent>();

                spriteRenderer.sprite = rocketEngine.enable ? _settings.rocket.sprites[1] : _settings.rocket.sprites[0];
            }
        }
    }
}