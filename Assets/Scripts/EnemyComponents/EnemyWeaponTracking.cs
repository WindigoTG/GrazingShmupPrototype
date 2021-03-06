using UnityEngine;

namespace GrazingShmup
{
    public class EnemyWeaponTracking : EnemyWeapon
    {
        #region ClassLifeCycles

        public EnemyWeaponTracking(BulletConfig config, IProjectile bullet) : base(config, bullet) { }

        #endregion


        #region ICloneable

        public override object Clone()
        {
            return new EnemyWeaponTracking(_config, _bullet);
        }

        #endregion


        #region IWeaponEnemy

        public override void Shoot(Transform bulletSpawn, Vector3 targetPosition)
        {
            if (Time.time - _lastFiredTime >= _config.FireDelay)
            {
                Quaternion rotation = new Quaternion();
                rotation.SetLookRotation(Vector3.forward, targetPosition - bulletSpawn.position);
                _bullet.Fire(_config, bulletSpawn.position, rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }

        #endregion
    }
}