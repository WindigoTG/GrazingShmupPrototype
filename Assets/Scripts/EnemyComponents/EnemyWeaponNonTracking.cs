using UnityEngine;

namespace GrazingShmup
{
    public class EnemyWeaponNonTracking : EnemyWeapon
    {
        #region ClassLifeCycles

        public EnemyWeaponNonTracking(BulletConfig config, IProjectile bullet) : base(config, bullet) { }

        #endregion


        #region ICloneable

        public override object Clone()
        {
            return new EnemyWeaponNonTracking(_config, _bullet);
        }

        #endregion


        # region IWeaponEnemy

        public override void Shoot(Transform bulletSpawn, Vector3 targetPosition)
        {
            if (Time.time - _lastFiredTime >= _config.FireDelay)
            {
                _bullet.Fire(_config, bulletSpawn.position, bulletSpawn.rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }

        #endregion
    }
}