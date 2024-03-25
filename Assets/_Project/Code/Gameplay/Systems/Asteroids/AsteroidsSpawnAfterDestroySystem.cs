using Infrastructure.ECS;

namespace Gameplay
{
    public class AsteroidsSpawnAfterDestroySystem : BaseSystem
    {
        private Mask _mask;

        protected override void OnInitialize()
        {
            Mask<DestroyEvent, AsteroidsAfterDestroyComponent>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _mask)
            {
                ref var asteroidSpawn = ref entity.GetComponent<AsteroidsAfterDestroyComponent>();

                entity.SetComponent(new AsteroidSpawnEvent
                {
                    asteroids = asteroidSpawn.asteroids
                });
            }
        }
    }
}