using UnityEngine;

namespace GrazingShmup
{
    public class SingleBullet : Projectile
    {
        private BulletOwner _owner;

        public SingleBullet(BulletOwner owner)
        {
            _owner = owner;
        }

        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().GetBulletPool(config.BulletPrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new BaseProjectileMoveCommand(bullet, _owner, config);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}