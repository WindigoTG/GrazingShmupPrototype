using UnityEngine;

namespace GrazingShmup
{
    public class BurstCapsule : Projectile
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command = new BurstCapsuleMoveCommand(bullet, config.BurstCapsuleSettings.BCapsuleSpeed,
                                                                        config.BurstCapsuleSettings.BCapsuleDeltaSpeed, config.BurstCapsuleSettings.BCapsuleDeltaSpeedDelay,
                                                                          config.BurstCapsuleSettings.BCapsuleTurnSpeed, config.BurstCapsuleSettings.BCapsuleLifeTime,
                                                                          _subProjectile, config, config.BurstCapsuleSettings.BurstRefireTime);


            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}