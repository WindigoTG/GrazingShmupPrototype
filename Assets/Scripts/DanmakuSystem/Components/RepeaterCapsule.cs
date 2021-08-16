using UnityEngine;

namespace GrazingShmup
{
    public class RepeaterCapsule : Projectile
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().GetBulletPool(config.RepeaterCapsuleSettings.RCapsulePrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new RepeaterCapsuleMoveCommand(bullet, BulletOwner.Enemy, config, _subProjectile);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}