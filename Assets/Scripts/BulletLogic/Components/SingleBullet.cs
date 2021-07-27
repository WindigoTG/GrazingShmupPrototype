using UnityEngine;

namespace GrazingShmup
{
    public class SingleBullet : Fireable
    {
        private BulletOwner _owner;

        public SingleBullet(BulletOwner owner)
        {
            _owner = owner;
        }

        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = _owner == BulletOwner.Enemy ? 
                                ServiceLocator.GetService<ObjectPoolManager>().EnemyBulletsPool.Pop().transform :
                                ServiceLocator.GetService<ObjectPoolManager>().PlayerBulletsPool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command = new BaseBulletMoveCommand(bullet, _owner, config.ProjectileSpeed, config.ProjectileDeltaSpeed, config.ProjectileDeltaSpeedDelay, config.ProjectileTurnSpeed, config.LifeTime);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}