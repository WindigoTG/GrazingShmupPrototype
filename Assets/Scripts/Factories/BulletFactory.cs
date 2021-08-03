namespace GrazingShmup
{
    public class BulletFactory : IBulletFactory
    {
        public Projectile GetBullet(BulletBase bulletBase, ProjectileComponent[] bulletComponents, BulletOwner owner)
        {
            Projectile fireable = GetBase(bulletBase, owner);
            if (bulletComponents != null)
                for (int i = 0; i < bulletComponents.Length; i++)
                    fireable = Decorate(fireable, bulletComponents[i]);
            return fireable;
        }

        private Projectile Decorate(Projectile fireable, ProjectileComponent component)
        {
            switch (component)
            {
                case ProjectileComponent.Arc:
                    return fireable.FiredInArc();
                case ProjectileComponent.Capsule:
                    return fireable.FiredInDelayedCapsule();
                case ProjectileComponent.RepeaterCapsule:
                    return fireable.FiredInMultishotCapsule();
                case ProjectileComponent.Line:
                    return fireable.FiredInLine();
                case ProjectileComponent.Row:
                    return fireable.FiredInRow();
                case ProjectileComponent.BurstCapsule:
                    return fireable.FiredInBurstCapsule();
                case ProjectileComponent.SpinningCapsule:
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
    }
}