using UnityEngine;

namespace GrazingShmup
{
    public class RepeaterCapsule : Fireable
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command = new RepeaterCapsuleMoveCommand(bullet, config.RepeaterCapsuleSettings.RCapsuleSpeed,
                                                                        config.RepeaterCapsuleSettings.RCapsuleDeltaSpeed, config.RepeaterCapsuleSettings.RCapsuleDeltaSpeedDelay,
                                                                          config.RepeaterCapsuleSettings.RCapsuleTurnSpeed, config.RepeaterCapsuleSettings.RCapsuleLifeTime,
                                                                          _subFireable, config, config.RepeaterCapsuleSettings.RCapsuleRefireTime);


            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }
}