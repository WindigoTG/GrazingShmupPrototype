namespace GrazingShmup
{
    public interface IMoveable
    {
        void Move(float inputHor, float inputVer, float deltaTime);
    }
}