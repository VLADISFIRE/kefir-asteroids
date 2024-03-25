using Infrastructure.ECS;

namespace Gameplay
{
    public class DeathSystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<HealthComponent>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _mask)
            {
                ref var health = ref entity.GetComponent<HealthComponent>();

                if (health.value <= 0)
                {
                    entity.AddComponent<DestroyEvent>();
                }
            }
        }
    }
}