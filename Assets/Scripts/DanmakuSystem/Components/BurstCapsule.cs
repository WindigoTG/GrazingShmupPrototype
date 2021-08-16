using UnityEngine;

namespace GrazingShmup
{
    public class BurstCapsule : Projectile
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().GetBulletPool(config.BurstCapsuleSettings.BCapsulePrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new BurstCapsuleMoveCommand(bullet, BulletOwner.Enemy, config, _subProjectile);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}