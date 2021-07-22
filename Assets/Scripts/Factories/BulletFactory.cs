using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class BulletFactory : IBulletFactory
    {
        public Fireable GetBullet(BulletComponent[] bulletComponents, BulletOwner owner)
        {
            Fireable fireable = new SingleBullet(owner);
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
                case BulletComponent.Capsule:
                    return fireable.FiredInDelayedCapsule();
                case BulletComponent.Line:
                    return fireable.FiredInLine();
                case BulletComponent.Row:
                    return fireable.FiredInRow();
                default:
                    return fireable;
            }
        }
    }
}