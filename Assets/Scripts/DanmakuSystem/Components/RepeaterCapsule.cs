using UnityEngine;

namespace GrazingShmup
{
    public class RepeaterCapsule : Projectile
    {
        #region IProjectile

        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().GetBulletPool(config.RepeaterCapsuleSettings.RCapsulePrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new RepeaterCapsuleMoveCommand(bullet, BulletOwner.Enemy, config, _subProjectile);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }

        #endregion
    }
}