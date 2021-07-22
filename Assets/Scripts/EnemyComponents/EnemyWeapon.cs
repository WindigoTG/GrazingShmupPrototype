using System;
using UnityEngine;

namespace GrazingShmup
{
    public abstract class EnemyWeapon : IWeaponEnemy, ICloneable
    {
        protected BulletConfig _config;
        protected IFireable _bullet;
        protected float _lastFiredTime = 0;

        public EnemyWeapon(BulletConfig config, IFireable bullet)
        {
            _config = config;
            _bullet = bullet;
        }

        public void InvertAngularSpeed()
        {
            _config.BulletAngularSpeed = -_config.BulletAngularSpeed * 180 / (float)Math.PI;
        }

        public abstract object Clone();
        public abstract void Shoot(Transform bulletSpawn, Vector3 targetPosition);
    }
}