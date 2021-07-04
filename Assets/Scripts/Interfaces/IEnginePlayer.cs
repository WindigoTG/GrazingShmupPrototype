using UnityEngine;

namespace GrazingShmup
{
    public interface IEnginePlayer
    {
        void Move(float inputHor, float inputVer, float deltaTime);
        void SetDependencies(Transform playerShip, PlayerData playerData);
    }
}