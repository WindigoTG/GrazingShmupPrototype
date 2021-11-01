using System.Collections.Generic;

namespace GrazingShmup
{
    public interface IBulletFactory
    {
        Projectile GetBullet(BulletBase bulletBase, List<BulletComponent> bulletComponents, BulletOwner owner);
    }
}