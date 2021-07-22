using UnityEngine;

namespace GrazingShmup
{
    public sealed class PlayerWeapon : IWeaponPlayer
    {
        private BulletConfig _config;
        private IFireable _projectile;
        private float _lastFiredTime;
        private float _fireDelay;

        private BulletConfig _configMinPower;

        public PlayerWeapon(BulletConfig config)
        {
            _projectile = new SingleBullet(BulletOwner.Player).FiredInRow().FiredInArc().FiredInLine();
            _config = config;
            _fireDelay = _config.FireDelay;
            _lastFiredTime = -_fireDelay;
            _configMinPower = GetMinPowerConfig(config);
        }

        public void Shoot(Transform bulletSpawn, bool isMaxPower)
        {
            if (Time.time - _lastFiredTime >= _fireDelay)
            {
                _projectile.Fire(isMaxPower ? _config : _configMinPower, bulletSpawn.position, bulletSpawn.rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }

        private BulletConfig GetMinPowerConfig(BulletConfig config)
        {
            return new BulletConfig(config.BulletSpeed, config.BulletAngularSpeed, config.LifeTime / 2,
                config.FireDelay * 2, config.LineBulletCount, config.DeltaSpeed,
                config.ArcLineCount/3, config.ArcAngle * 180 / (float)Mathf.PI / 3, 0,
                config.RowLineCount / 2, config.RowLineOffset, config.RowVerticalOffset, config.IsMirrored,
                config.CapsuleDelay, config.CapsuleSpeed, config.CapsuleAngularSpeed);
        }
    }
}