using Infrastructure.DI;

namespace Initializers.Presenters
{
    public class PresentersInitializer
    {
        public PresentersInitializer(Scope scope)
        {
            scope.Register<GameoverPresenter>();
            scope.Register<HudPresenter>();
        }
    }
}