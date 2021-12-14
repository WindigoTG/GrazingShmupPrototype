using UnityEngine;

namespace GrazingShmup
{
    public sealed class PlayerWeapon : IWeaponPlayer
    {
        #region Fields

        private BulletConfig[] _config;
        private IProjectile _projectile;
        private float _lastFiredTime;
        private float _fireDelay;

        #endregion


        #region ClassLifeCycles

        public PlayerWeapon(BulletConfig[] config, IProjectile projectile)
        {
            _config = config;
            _projectile = projectile;
            _fireDelay = _config[0].FireDelay;
            _lastFiredTime = -_fireDelay;
        }

        #endregion


        #region IWeaponPlayer

        public void Shoot(Transform bulletSpawn, int powerLevel)
        {
            if (Time.time - _lastFiredTime >= _fireDelay)
            {
                _projectile.Fire(_config[powerLevel], bulletSpawn.position, bulletSpawn.rotation.eulerAngles);
                _lastFiredTime = Time.time;
                _fireDelay = _config[powerLevel].FireDelay;
            }
        }

        #endregion
    }
}