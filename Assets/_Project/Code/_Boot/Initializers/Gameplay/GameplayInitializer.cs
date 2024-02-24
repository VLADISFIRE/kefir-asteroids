using Gameplay;
using Infrastructure.DI;
using Infrastructure.ECS;
using UnityEngine;

namespace Game.Gameplay
{
    public class GameplayInitializer
    {
        private const string CONFIG_PATH = "GameSettings";

        private Scope _scope;
        private IEcsManager _ecsManager;

        public GameplayInitializer(Scope scope, IEcsManager ecsManager)
        {
            _ecsManager = ecsManager;

            _scope = new Scope(scope);

            var settings = Resources.Load<GameSettingsScrobject>(CONFIG_PATH);
            scope.RegisterInstance(settings);
            
            _ecsManager.AddSystem<PlayerInitSystem>();
            

            _ecsManager.AddSystem<PlayerInputReaderSystem>();
            
            _ecsManager.AddSystem<MovementSystem>();
            
        }
    }
}