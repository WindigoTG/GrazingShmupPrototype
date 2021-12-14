using UnityEngine;

namespace GrazingShmup
{
    public class SingleBullet : Projectile
    {
        #region Fields

        private BulletOwner _owner;

        #endregion


        #region ClassLifeCycles

        public SingleBullet(BulletOwner owner)
        {
            _owner = owner;
        }

        #endregion


        #region IProjectile

        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().GetBulletPool(config.BulletPrefab).Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IProjectileMoveCommand command = new BaseProjectileMoveCommand(bullet, _owner, config);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }

        #endregion
    }
}