using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInputReaderSystem : BaseSystem
    {
        private GameInput _input;

        private Mask _mask;

        public PlayerInputReaderSystem(GameInput input)
        {
            _input = input;
        }

        protected override void OnInitialize()
        {
            Mask<PlayerTag, MovementComponent>().Build(out _mask);

            _input.Player.Enable();
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (!_input.Player.enabled) return;

            foreach (var entity in _mask)
            {
                ref var move = ref entity.GetComponent<MovementComponent>();

                var direction = _input.Player.Move.ReadValue<Vector2>();
                
                //Only forward)
                direction.x = Mathf.Abs(direction.x);
                
                move.velocity = direction;
            }
        }
    }
}