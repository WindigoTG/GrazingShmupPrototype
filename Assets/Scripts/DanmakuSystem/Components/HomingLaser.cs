using UnityEngine;

namespace GrazingShmup
{ 
public class HomingLaser : Projectile
    {
        private BulletOwner _owner;

        public HomingLaser(BulletOwner owner)
        {
            _owner = owner;
        }

        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
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
    }   
}