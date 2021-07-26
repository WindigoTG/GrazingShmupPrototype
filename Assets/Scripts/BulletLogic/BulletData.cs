using UnityEngine;

namespace GrazingShmup
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public sealed class BulletData : ScriptableObject
    {
        [SerializeField] BulletConfig _bulletConfig;
        [Space]
        [SerializeField] private BulletBase _bulletBase;
        [SerializeField] private BulletOwner _bulletOwner;
        [SerializeField] private bool _enemyWeaponTracking;
        [Space]
        [SerializeField] private BulletComponent[] _bulletComponents;

        public BulletConfig Config => _bulletConfig;
        public BulletBase Base => _bulletBase;
        
        public BulletComponent[] BulletComponents => _bulletComponents;

        public BulletOwner BulletOwner => _bulletOwner;
        public bool IsTracking => _enemyWeaponTracking;
    }
}