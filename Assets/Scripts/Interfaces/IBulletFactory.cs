namespace GrazingShmup
{
    public interface IBulletFactory
    {
        Fireable GetBullet(BulletComponent[] bulletComponents, BulletOwner owner);
    }
}