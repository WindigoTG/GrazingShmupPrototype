using UnityEngine;

namespace GrazingShmup
{
    public class Capsule : Projectile
    {
        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = _objectPoolManager.GetBulletPool(config.CapsuleSettings.CapsulePrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new DelayedCapsuleMoveCommand(bullet, BulletOwner.Enemy, config, _subProjectile);

            _bulletManger.AddCommand(command);
        }
    }
}