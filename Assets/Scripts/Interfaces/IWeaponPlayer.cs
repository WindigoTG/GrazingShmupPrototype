using UnityEngine;

namespace GrazingShmup
{
    public interface IWeaponPlayer
    {
        void Shoot(Transform bulletSpawn);
    }
}