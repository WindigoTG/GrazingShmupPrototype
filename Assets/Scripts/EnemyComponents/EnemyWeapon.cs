using System;
using UnityEngine;

namespace GrazingShmup
{
    public abstract class EnemyWeapon : IWeaponEnemy, ICloneable
    {
        protected ProjectileConfig _config;
        protected IProjectile _bullet;
        protected float _lastFiredTime = 0;

        public EnemyWeapon(ProjectileConfig config, IProjectile bullet)
        {
            _config = config;
            _bullet = bullet;
        }

        public void InvertAngularSpeed()
        {
            _config.ProjectileTurnSpeed = -_config.ProjectileTurnSpeed * 180 / (float)Math.PI;
        }

        public abstract object Clone();
        public abstract void Shoot(Transform bulletSpawn, Vector3 targetPosition);
    }
}