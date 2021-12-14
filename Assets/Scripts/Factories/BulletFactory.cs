using System.Collections.Generic;

namespace GrazingShmup
{
    public class BulletFactory : IBulletFactory
    {
        #region IBulletFactory

        public Projectile GetBullet(BulletBase bulletBase, List<BulletComponent> bulletComponents, BulletOwner owner)
        {
            Projectile fireable = GetBase(bulletBase, owner);
            if (bulletComponents != null)
                for (int i = 0; i < bulletComponents.Count; i++)
                    fireable = Decorate(fireable, bulletComponents[i]);
            return fireable;
        }

        #endregion


        #region Methods

        private Projectile Decorate(Projectile fireable, BulletComponent component)
        {
            switch (component)
            {
                case BulletComponent.Arc:
                    return fireable.FiredInArc();
                case BulletComponent.Capsule:
                    return fireable.FiredInDelayedCapsule();
                case BulletComponent.RepeaterCapsule:
                    return fireable.FiredInMultishotCapsule();
                case BulletComponent.Line:
                    return fireable.FiredInLine();
                case BulletComponent.Row:
                    return fireable.FiredInRow();
                case BulletComponent.BurstCapsule:
                    return fireable.FiredInBurstCapsule();
                case BulletComponent.SpinningCapsule:
                    return fireable.FiredInSpinningCapsule();
                default:
                    return fireable;
            }
        }

        private Projectile GetBase(BulletBase bulletBase, BulletOwner owner)
        {
            switch (bulletBase)
            {
                case BulletBase.Bullet:
                default:
                    return new SingleBullet(owner);
                case BulletBase.HomingLaser:
                    return new HomingLaser(owner);
            }
        }

        #endregion
    }
}