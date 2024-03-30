using Gameplay;
using Infrastructure.ECS;
using UI;

public class GameoverUISystem : BaseSystem
{
    private Gameover _gameover;
    private GameoverWindow _window;

    public GameoverUISystem(GameoverWindow window, Gameover gameover)
    {
        _window = window;
        _gameover = gameover;
    }

    protected override void OnLateUpdate()
    {
        if (_gameover.enable)
        {
            _window.Show();
        }
        else
        {
            _window.Hide();
        }
    }
}