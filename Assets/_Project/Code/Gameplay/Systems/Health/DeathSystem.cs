using Infrastructure.ECS;

namespace Gameplay
{
    public class DeathSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<HealthComponent>().Exclude<DestroyEvent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var health = ref entity.GetComponent<HealthComponent>();

                if (health.value > 0) continue;

                entity.AddComponent<DestroyEvent>();
            }
        }
    }
}