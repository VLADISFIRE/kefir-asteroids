using Infrastructure.ECS;

namespace Gameplay
{
    public class DamageSystem : BaseEventSystem<DamageEvent>
    {
        private Mask _mask;

        protected override void OnInitialized()
        {
            Mask<HealthComponent, DamageEvent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var health = ref entity.GetComponent<HealthComponent>();
                ref var damage = ref entity.GetComponent<DamageEvent>();

                health.value -= damage.value;
            }
        }
    }
}