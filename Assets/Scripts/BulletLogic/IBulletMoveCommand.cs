namespace GrazingShmup
{
    public interface IBulletMoveCommand
    {
        public void Execute(float deltaTime);
    }
}