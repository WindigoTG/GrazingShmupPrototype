using UnityEngine;

namespace GrazingShmup
{
    public class BurstCapsule : Projectile
    {
        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = _objectPoolManager.GetBulletPool(config.BurstCapsuleSettings.BCapsulePrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new BurstCapsuleMoveCommand(bullet, BulletOwner.Enemy, config, _subProjectile);

            _bulletManger.AddCommand(command);
        }
    }
}