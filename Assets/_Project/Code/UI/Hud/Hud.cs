using System;
using Gameplay;
using UnityEngine;

namespace UI
{
    public class Hud : UIElement, IDisposable
    {
        private HudLayout _layout;
        private ScoreSystem _scoreSystem;
        private RocketObserverSystem _rocketObserverSystem;
        private RocketLaserWeaponSystem _laserWeaponSystem;

        public Hud(HudLayout layout, ScoreSystem scoreSystem, RocketObserverSystem rocketObserverSystem,
            RocketLaserWeaponSystem laserWeaponSystem) : base(
            layout.gameObject)
        {
            _layout = layout;

            _scoreSystem = scoreSystem;
            _scoreSystem.scoreChanged += SetScore;

            _rocketObserverSystem = rocketObserverSystem;
            _rocketObserverSystem.positionChanged += SetPosition;
            _rocketObserverSystem.rotationChanged += SetRotation;
            _rocketObserverSystem.velocityChanged += SetVelocity;

            _laserWeaponSystem = laserWeaponSystem;
            _laserWeaponSystem.chargesChanged += SetCharges;
            _laserWeaponSystem.cooldownChanged += SetCooldown;
        }

        public void Dispose()
        {
            _scoreSystem.scoreChanged -= SetScore;

            _rocketObserverSystem.positionChanged -= SetPosition;
            _rocketObserverSystem.rotationChanged -= SetRotation;
            _rocketObserverSystem.velocityChanged -= SetVelocity;

            _laserWeaponSystem.chargesChanged -= SetCharges;
            _laserWeaponSystem.cooldownChanged -= SetCooldown;
        }

        protected override void OnShow()
        {
            SetScore(_scoreSystem.score);

            SetPosition(_rocketObserverSystem.position);
            SetRotation(_rocketObserverSystem.rotation);
            SetVelocity(_rocketObserverSystem.velocity);
            
            SetCharges(_laserWeaponSystem.charges);
            SetCooldown(_laserWeaponSystem.cooldown);
        }

        private void SetPosition(Vector2 position)
        {
            _layout.position.text = $"Position: {position}";
        }

        private void SetRotation(Vector2 rotation)
        {
            var angle = Vector2.Angle(rotation, Vector2.up);
            angle = Mathf.Ceil(angle);
            _layout.angle.text = $"Angle: {angle} (deg)";
        }

        private void SetVelocity(Vector2 velocity)
        {
            var magnitude = velocity.magnitude;
            _layout.velocity.text = $"Velocity: {magnitude:F2}";
        }

        private void SetScore(int value)
        {
            _layout.score.text = $"Score: {value}";
        }

        private void SetCooldown(float cooldown)
        {
            _layout.cooldown.text = $"Laser Cooldown: {cooldown:F2}";
        }

        private void SetCharges(int charges)
        {
            _layout.charges.text = $"Laser Charges: {charges}";
        }
    }
}