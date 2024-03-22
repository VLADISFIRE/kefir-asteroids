using Gameplay.Factory;
using Infrastructure.ECS;
using UnityEngine;

namespace Gameplay
{
    public class AsteroidSpawnerSystem : BaseSystem
    {
    
        private float _time;
        private AsteroidFactory _factory;

        public AsteroidSpawnerSystem(GameSettingsScrobject settings, AsteroidFactory factory)
        {
            _factory = factory;
        }

        // protected override void OnUpdate(float deltaTime)
        // {
        //     _time += deltaTime;
        //
        //     if (_time > _settings.spawner.delay)
        //     {
        //         var random = Random.Range(0, 4);
        //
        //         switch (random)
        //         {
        //             case 0:
        //                 _factory.Create();
        //                 break;
        //         }
        //
        //         _time = 0;
        //     }
        // }
        //
        // private void SpawnBigAsteroid()
        // {
        //     var entity = _world.NewEntity();
        //
        //     entity.AddComponent<AsteroidTag>();
        //
        //     entity.SetComponent(new TransformComponent()
        //     {
        //         position = 
        //     });
        //
        //     entity.SetComponent(new MovementComponent()
        //     {
        //         velocity = new Vector2(0, 1)
        //     });
        // }
    }
}