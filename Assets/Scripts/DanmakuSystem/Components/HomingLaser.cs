using UnityEngine;

namespace GrazingShmup
{ 
public class HomingLaser : Projectile
    {
        #region

        private BulletOwner _owner;

        #endregion


        #region ClassLifeCycles

        public HomingLaser(BulletOwner owner)
        {
            _owner = owner;
        }

        #endregion


        #region IProjectile

        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().GetBulletPool(config.HomingLaserPrefab).Pop().transform;

            var trail = bullet.GetComponent<TrailRenderer>();
            trail.Clear();

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            trail.Clear();

            IProjectileMoveCommand command = new HomingLaserMoveCommand(bullet, _owner, config);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }

        #endregion
    }
}