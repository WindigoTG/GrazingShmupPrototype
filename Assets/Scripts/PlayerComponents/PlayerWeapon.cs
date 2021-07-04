using UnityEngine;

namespace GrazingShmup
{
    public sealed class PlayerWeapon : IWeaponPlayer
    {
        private BullletConfig _config;
        private IFireable _projectile;
        private float _lastFiredTime;
        private float _fireDelay;

        public PlayerWeapon(BullletConfig config)
        {
            _projectile = new SingleBullet(BulletOwner.Player).FiredInRow().FiredInArc().FiredInLine();
            _config = config;
            _fireDelay = _config.FireDelay;
            _lastFiredTime = -_fireDelay;
        }

        public void Shoot(Transform bulletSpawn)
        {
            if (Time.time - _lastFiredTime >= _fireDelay)
            {
                _projectile.Fire(_config, bulletSpawn.position, bulletSpawn.rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }
    }
}