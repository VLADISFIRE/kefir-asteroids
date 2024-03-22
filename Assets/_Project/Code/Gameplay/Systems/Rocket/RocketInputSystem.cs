using Infrastructure.ECS;

namespace Gameplay
{
    public class RocketInputSystem : BaseSystem
    {
        private GameInput.PlayerActions _input;

        private Mask _mask;

        public RocketInputSystem(GameInput input)
        {
            _input = input.Player;
        }

        protected override void OnInitialize()
        {
            Mask<RocketEngineComponent, RocketRotateControlComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (!_input.enabled) return;

            foreach (var entity in _mask)
            {
                ref var rocketEngine = ref entity.GetComponent<RocketEngineComponent>();
                ref var rocketRotateControl = ref entity.GetComponent<RocketRotateControlComponent>();

                var inputRocketThrust = _input.RocketThrust;
                var pressed = inputRocketThrust.IsPressed();
                rocketEngine.enable = pressed;
                
                var readValue = _input.RocketRotate.ReadValue<float>();
                rocketRotateControl.enable = readValue != 0;
                rocketRotateControl.left = readValue > 0;
            }
        }
    }
}