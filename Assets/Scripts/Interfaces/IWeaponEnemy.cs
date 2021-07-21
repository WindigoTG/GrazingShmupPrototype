using UnityEngine;

namespace GrazingShmup
{
    public interface IWeaponEnemy
    {
        void Shoot(Transform bulletSpawn, Vector3 targetPosition);
    }
}