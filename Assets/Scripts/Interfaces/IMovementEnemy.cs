using UnityEngine;

namespace GrazingShmup
{
    public interface IMovementEnemy
    {
        void Move(float deltaTime);
        void SetObjectToMoveAndRoute(Transform enemy, Vector3[] positions);
    }
}