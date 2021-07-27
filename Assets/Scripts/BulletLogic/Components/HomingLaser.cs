using UnityEngine;

namespace GrazingShmup
{ 
public class HomingLaser : Fireable
    {
        private BulletOwner _owner;

        public HomingLaser(BulletOwner owner)
        {
            _owner = owner;
        }

        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().HomingLaserPool.Pop().transform;

            var trail = bullet.GetComponent<TrailRenderer>();
            trail.Clear();

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            trail.Clear();

            IBulletMoveCommand command = new HomingLaserMoveCommand(bullet, _owner, config.ProjectileSpeed, config.ProjectileDeltaSpeed, config.ProjectileDeltaSpeedDelay,
                                                                    config.ProjectileTurnSpeed, config.HomingTime, config.LifeTime);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }   
}