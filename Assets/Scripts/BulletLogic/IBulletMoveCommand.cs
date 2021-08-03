namespace GrazingShmup
{
    public interface IBulletMoveCommand
    {
        public bool Execute(float deltaTime);
    }
}