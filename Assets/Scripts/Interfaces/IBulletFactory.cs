namespace GrazingShmup
{
    public interface IBulletFactory
    {
        Fireable GetBullet(BulletBase bulletBase, BulletComponent[] bulletComponents, BulletOwner owner);
    }
}