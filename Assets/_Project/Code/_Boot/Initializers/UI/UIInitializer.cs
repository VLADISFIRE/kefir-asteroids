using Infrastructure.DI;
using Infrastructure.ECS;
using UI;
using UnityEngine;

namespace Initializers.UI
{
    public class UIInitializer
    {
        private const string CONFIG_PATH = "UISettings";

        private const string CANVAS_TAG = "Canvas";

        public UIInitializer(Scope scope, IEcsManager manager)
        {
            var settings = Resources.Load<UISettingsScrobject>(CONFIG_PATH);

            scope.RegisterInstance(settings);

            var canvas = GameObject.FindGameObjectWithTag(CANVAS_TAG);

            var gameoverLayout = Object.Instantiate(settings.prefabs.gameOver, canvas.transform);
            gameoverLayout.gameObject.SetActive(false);
            scope.RegisterInstance(gameoverLayout);

            scope.Register<GameoverWindow>();

            var hudLayout = Object.Instantiate(settings.prefabs.hud, canvas.transform);
            hudLayout.gameObject.SetActive(false);
            scope.RegisterInstance(hudLayout);

            scope.Register<Hud>();

            manager.AddSystem<HudUISystem>(EngineType.Default);
            manager.AddSystem<GameoverUISystem>(EngineType.Default);
        }
    }
}