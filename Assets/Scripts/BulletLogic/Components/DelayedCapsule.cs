using UnityEngine;

namespace GrazingShmup
{
    public class DelayedCapsule : Fireable
    {
        public override void Fire(BullletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command = new DelayedCapsuleMoveCommand(bullet, config.CapsuleSpeed, config.CapsuleAngularSpeed, config.CapsuleDelay, _subFireable, config);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}