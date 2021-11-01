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
            Transform bullet = _objectPoolManager.GetBulletPool(config.BulletPrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new BaseProjectileMoveCommand(bullet, _owner, config);

            _bulletManger.AddCommand(command);
        }
    }
}