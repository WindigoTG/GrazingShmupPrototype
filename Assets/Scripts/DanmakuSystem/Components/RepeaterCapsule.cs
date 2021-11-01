using UnityEngine;

namespace GrazingShmup
{
    public class RepeaterCapsule : Projectile
    {
        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = _objectPoolManager.GetBulletPool(config.RepeaterCapsuleSettings.RCapsulePrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new RepeaterCapsuleMoveCommand(bullet, BulletOwner.Enemy, config, _subProjectile);

            _bulletManger.AddCommand(command);
        }
    }
}