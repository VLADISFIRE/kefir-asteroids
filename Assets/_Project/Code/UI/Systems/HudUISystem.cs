using Gameplay;
using Infrastructure.ECS;
using UI;
using UnityEngine;

public class HudUISystem : BaseSystem
{
    private Hud _hud;

    private Mask _mask;
    private Gameover _gameover;

    public HudUISystem(Hud hud, Gameover gameover)
    {
        _gameover = gameover;
        _hud = hud;
    }

    protected override void OnInitialize()
    {
        Mask<TransformComponent, RocketLaserComponent, MovementComponent>().Build(out _mask);
    }

    protected override void OnPlayed()
    {
        _hud.SetPosition(Vector2.zero);
        _hud.SetRotation(Vector2.zero);

        _hud.SetVelocity(Vector2.zero);

        _hud.SetCharges(0);
        _hud.SetCooldown(0);
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (_gameover.enable)
        {
            _hud.Hide();
        }
        else
        {
            _hud.Show();
        }

        if (!_hud.active) return;

        foreach (var entity in _mask)
        {
            ref var transform = ref entity.GetComponent<TransformComponent>();

            _hud.SetPosition(transform.position);
            _hud.SetRotation(transform.rotation);

            ref var movement = ref entity.GetComponent<MovementComponent>();

            _hud.SetVelocity(movement.velocity);

            ref var laser = ref entity.GetComponent<RocketLaserComponent>();

            _hud.SetCharges(laser.charges);
            _hud.SetCooldown(laser.newChargeCooldown);
        }
    }
}