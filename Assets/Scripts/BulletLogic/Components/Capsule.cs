using UnityEngine;

namespace GrazingShmup
{
    public class Capsule : Projectile
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command = new DelayedCapsuleMoveCommand(bullet, config.CapsuleSettings.CapsuleSpeed, config.CapsuleSettings.CapsuleDeltaSpeed, config.CapsuleSettings.CapsuleDeltaSpeedDelay,
                                                                       config.CapsuleSettings.CapsuleTurnSpeed, config.CapsuleSettings.CapsuleLifeTime, _subProjectile, config);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}