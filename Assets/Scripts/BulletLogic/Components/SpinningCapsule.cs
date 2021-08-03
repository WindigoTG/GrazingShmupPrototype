using UnityEngine;

namespace GrazingShmup
{
    public class SpinningCapsule : Projectile
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command = new SpinningCapsuleMoveCommand(bullet, config.SpinningCapsuleSettings.SCapsuleSpeed,
                                                                        config.SpinningCapsuleSettings.SCapsuleDeltaSpeed, config.SpinningCapsuleSettings.SCapsuleDeltaSpeedDelay,
                                                                          config.SpinningCapsuleSettings.SCapsuleTurnSpeed, 0,
                                                                          _subProjectile, config);


            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}