using Infrastructure.ECS;

namespace Gameplay
{
    public class SpawnParticleAfterDestroySystem : BaseEventSystem<ParticleEvent>
    {
        private Mask _mask;

        protected override void OnInitialized()
        {
            Mask<DestroyEvent, TransformComponent, ParticleTag>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _mask)
            {
                ref var transform = ref entity.GetComponent<TransformComponent>();
                
                entity.SetComponent(new ParticleEvent
                {
                    position = transform.position
                });
            }
        }
    }
}