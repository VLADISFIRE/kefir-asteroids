using Gameplay;
using Gameplay.Render;
using Infrastructure.DI;
using Infrastructure.ECS;
using UnityEngine;

namespace Initializers.Gameplay
{
    public class GameplayInitializer
    {
        private const string CAMERA_TAG = "MainCamera";

        private const string CONFIG_PATH = "GameSettings";

        private Scope _scope;

        public GameplayInitializer(Scope scope, IEcsManager ecsManager)
        {
            _scope = new Scope(scope);

            var cameraObj = GameObject.FindGameObjectWithTag(CAMERA_TAG);

            if (cameraObj.TryGetComponent(out Camera camera))
            {
                scope.RegisterInstance(camera);
            }

            var scrobject = Resources.Load<GameSettingsScrobject>(CONFIG_PATH);
            scope.RegisterInstance(scrobject.settings);

            scope.Register<IScreen, UnityScreen>();

            ecsManager
               .AddSystem<RocketInputSystem>();

            scope.Register<ICollisionMatrix, CollisionMatrix>();

            ecsManager
               .AddSystem<PhysicsSystem>()
               .AddSystem<CollisionSystem>()
               .AddSystem<MovementSystem>();

            ecsManager
               .AddSystem<RocketEngineSystem>()
               .AddSystem<RocketRotateControlSystem>()
               .AddSystem<RocketVelocityFrictionSystem>()
               .AddSystem<RocketAngleVelocityFrictionSystem>()
               .AddSystem<RocketWeaponSystem>()
               .AddSystem<RocketPistolWeaponSystem>();

            ecsManager
               .AddSystem<DamageCollisionSystem>()
               .AddSystem<DamageSystem>()
               .AddSystem<DeathSystem>();

            ecsManager
               .AddSystem<RocketSpawnSystem>()
               .AddSystem<AsteroidsSpawnAfterDestroySystem>()
               .AddSystem<AsteroidsSpawnSystem>();

            ecsManager
               .AddSystem<ScreenPortalSystem>()
               .AddSystem<DestroyScreenOutsidersSystem>()
               .AddSystem<SpawnParticleAfterDestroySystem>();

            //Render
            var spriteRendererPool = new SpriteRendererPool(8);
            scope.RegisterInstance(spriteRendererPool);

            var particlePool = new ParticleSystemPool(scrobject.settings.particle, 8);
            scope.RegisterInstance(particlePool);

            ecsManager
               .AddSystem<ParticleSpawnSystem>()
               .AddSystem<SpriteRendererSystem>(EngineType.Default)
               .AddSystem<RocketRenderSystem>(EngineType.Default);

            ecsManager
               .AddSystem<DestroySystem>();
        }
    }
}