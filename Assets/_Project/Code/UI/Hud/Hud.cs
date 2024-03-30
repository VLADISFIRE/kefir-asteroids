using System;
using Gameplay;
using UnityEngine;

namespace UI
{
    public class Hud : UIElement, IDisposable
    {
        private PlayerScore _score;

        private HudLayout _layout;

        public Hud(HudLayout layout, PlayerScore score) : base(
            layout.gameObject)
        {
            _score = score;
            _layout = layout;

            SetScore(score.value);
            _score.updated += SetScore;
        }

        public void Dispose()
        {
            _score.updated -= SetScore;
        }

        public void SetPosition(Vector2 position)
        {
            _layout.position.text = $"Position: {position}";
        }

        public void SetRotation(Vector2 rotation)
        {
            var angle = Vector2.Angle(rotation, Vector2.up);
            angle = Mathf.Ceil(angle);
            _layout.angle.text = $"Angle: {angle} (deg)";
        }

        public void SetVelocity(Vector2 velocity)
        {
            var magnitude = velocity.magnitude;
            _layout.velocity.text = $"Velocity: {magnitude:F2}";
        }

        public void SetScore(int value)
        {
            _layout.score.text = $"Score: {value}";
        }

        public void SetCooldown(float cooldown)
        {
            cooldown = Mathf.Clamp(cooldown, 0, cooldown);
            _layout.cooldown.text = $"Laser Cooldown: {cooldown:F2}";
        }

        public void SetCharges(int charges)
        {
            _layout.charges.text = $"Laser Charges: {charges}";
        }
    }
}