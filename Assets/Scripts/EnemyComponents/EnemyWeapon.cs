using System;
using UnityEngine;

namespace GrazingShmup
{
    public abstract class EnemyWeapon : IWeaponEnemy, ICloneable
    {
        #region Fields

        protected BulletConfig _config;
        protected IProjectile _bullet;
        protected float _lastFiredTime = 0;

        #endregion


        #region ClassLifeCycles

        public EnemyWeapon(BulletConfig config, IProjectile bullet)
        {
            _config = config;
            _bullet = bullet;
        }

        #endregion


        #region Methods

        public void InvertAngularSpeed()
        {
            _config.BulletTurnSpeed = -_config.BulletTurnSpeed * 180 / (float)Math.PI;
        }

        #endregion


        #region ICloneable

        public abstract object Clone();

        #endregion


        #region IWeaponEnemy

        public abstract void Shoot(Transform bulletSpawn, Vector3 targetPosition);

        #endregion
    }
}