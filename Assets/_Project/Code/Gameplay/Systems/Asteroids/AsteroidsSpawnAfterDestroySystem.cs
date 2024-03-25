using Infrastructure.ECS;

namespace Gameplay
{
    public class AsteroidsSpawnAfterDestroySystem : BaseEventSystem<AsteroidSpawnEvent>
    {
        private Mask _mask;

        protected override void OnInitialized()
        {
            Mask<AsteroidsSpawnComponent, DestroyEvent>().Build(out _mask);
        }

        protected override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _mask)
            {
                ref var asteroidSpawn = ref entity.GetComponent<AsteroidsSpawnComponent>();

                entity.SetComponent(new AsteroidSpawnEvent
                {
                    asteroids = asteroidSpawn.asteroids
                });
            }
        }
    }
}