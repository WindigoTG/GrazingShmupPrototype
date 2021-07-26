using UnityEngine;

namespace GrazingShmup
{
    public class DelayedCapsule : Fireable
    {
        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command = config.IsRefireing ?
                new RefireCapsuleMoveCommand(bullet, config.CapsuleSpeed, config.CapsuleAngularSpeed, config.CapsuleDelay, _subFireable, config, config.RefireTime) :
                new DelayedCapsuleMoveCommand(bullet, config.CapsuleSpeed, config.CapsuleAngularSpeed, config.CapsuleDelay, _subFireable, config);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}