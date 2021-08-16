using UnityEngine;

namespace GrazingShmup
{
    public class SpinningCapsule : Projectile
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().CapsulePool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new SpinningCapsuleMoveCommand(bullet, BulletOwner.Enemy, config, _subProjectile);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}