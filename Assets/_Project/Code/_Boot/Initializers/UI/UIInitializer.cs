using Infrastructure.DI;
using UnityEngine;

namespace Game.UI
{
    public class UIInitializer
    {
        private readonly Scope _scope;
        private const string CONFIG_PATH = "UISettings";

        private const string CANVAS_TAG = "Canvas";

        public UIInitializer(Scope scope)
        {
            _scope = new Scope(scope);

            var settings = Resources.Load<UISettingsScrobject>(CONFIG_PATH);

            _scope.RegisterInstance(settings);

            var canvas = GameObject.FindGameObjectWithTag(CANVAS_TAG);

            var startWindowLayout = Object.Instantiate(settings.windows.start, canvas.transform);

            _scope.RegisterInstance(startWindowLayout);
            
            _scope.Register<StartWindow>();
        }
    }
}