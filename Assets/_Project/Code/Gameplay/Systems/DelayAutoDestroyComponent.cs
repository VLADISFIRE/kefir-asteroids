using Infrastructure.ECS;

namespace Gameplay
{
    public class DelayDestroySystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<DelayDestroyComponent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var delay = ref entity.GetComponent<DelayDestroyComponent>();

                delay.time -= deltaTime;
                if (delay.time <= 0)
                {
                    entity.AddComponent<DestroyEvent>();
                }
            }
        }
    }
}