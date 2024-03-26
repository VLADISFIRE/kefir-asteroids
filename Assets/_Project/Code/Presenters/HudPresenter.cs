using System;
using Gameplay;
using UI;

public class HudPresenter : IDisposable
{
    private GameoverSystem _system;
    private GameoverWindow _window;
    
    private Hud _hud;

    public HudPresenter(Hud hud, GameoverSystem system)
    {
        _hud = hud;
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
        if (!value)
        {
            _hud.Show();
        }
        else
        {
            _hud.Hide();
        }
    }
}