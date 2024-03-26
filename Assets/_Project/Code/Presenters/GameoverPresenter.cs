using System;
using Gameplay;
using UI;

public class GameoverPresenter : IDisposable
{
    private GameoverSystem _system;
    private GameoverWindow _window;

    public GameoverPresenter(GameoverWindow window, GameoverSystem system)
    {
        _window = window;
        _system = system;

        HandleChanged(_system.gameover);
        _system.changed += HandleChanged;
    }

    public void Dispose()
    {
        _system.changed -= HandleChanged;
    }

    public void Show()
    {
        _window.Show();
    }

    private void HandleChanged(bool value)
    {
        if (value)
        {
            _window.Show();
        }
        else
        {
            _window.Hide();
        }
    }
}