namespace GrazingShmup
{
    public class BulletFactory : IBulletFactory
    {
        public Fireable GetBullet(BulletBase bulletBase, BulletComponent[] bulletComponents, BulletOwner owner)
        {
            Fireable fireable = GetBase(bulletBase, owner);
            if (bulletComponents != null)
                for (int i = 0; i < bulletComponents.Length; i++)
                    fireable = Decorate(fireable, bulletComponents[i]);
            return fireable;
        }

        private Fireable Decorate(Fireable fireable, BulletComponent component)
        {
            switch (component)
            {
                case BulletComponent.Arc:
                    return fireable.FiredInArc();
                case BulletComponent.DelayedCapsule:
                    return fireable.FiredInDelayedCapsule();
                case BulletComponent.Line:
                    return fireable.FiredInLine();
                case BulletComponent.Row:
                    return fireable.FiredInRow();
                default:
                    return fireable;
            }
        }

        private Fireable GetBase(BulletBase bulletBase, BulletOwner owner)
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