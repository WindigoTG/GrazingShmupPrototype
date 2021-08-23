using UnityEngine;

namespace GrazingShmup
{
    public static class ScreenBounds
    {
        public static bool CheckIfWithinBounds(Vector3 position, float outOfBoundsOffset = 0)
        {
            return (position.x < UpperRight.x + outOfBoundsOffset) &&
                   (position.x > LowerLeft.x - outOfBoundsOffset) &&
                   (position.y < UpperRight.y + outOfBoundsOffset) &&
                   (position.y > LowerLeft.y - outOfBoundsOffset);
        }

        public static Vector3 LowerLeft => Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));

        public static Vector3 UpperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));

        public static float LeftBound => LowerLeft.x;

        public static float RightBound => UpperRight.x;

        public static float TopBound => UpperRight.y;

        public static float BottomBound => LowerLeft.y;
    }
}