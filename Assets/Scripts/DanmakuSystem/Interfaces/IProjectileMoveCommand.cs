namespace GrazingShmup
{
    public interface IProjectileMoveCommand
    {
        public bool Execute(float deltaTime);
    }
}