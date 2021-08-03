namespace GrazingShmup
{
    public interface IBulletFactory
    {
        Projectile GetBullet(BulletBase bulletBase, ProjectileComponent[] bulletComponents, BulletOwner owner);
    }
}