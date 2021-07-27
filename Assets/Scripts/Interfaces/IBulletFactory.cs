namespace GrazingShmup
{
    public interface IBulletFactory
    {
        Fireable GetBullet(BulletBase bulletBase, ProjectileComponent[] bulletComponents, BulletOwner owner);
    }
}