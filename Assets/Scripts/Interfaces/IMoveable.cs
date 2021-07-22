namespace GrazingShmup
{
    public interface IMoveable
    {
        void Move(float inputHor, float inputVer, bool isSlowedDown, float deltaTime);
    }
}