using UnityEngine;

namespace GrazingShmup
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public sealed class BulletData : ScriptableObject
    {
        [SerializeField] ProjectileConfig _bulletConfig;
        [Space]
        [SerializeField] private BulletBase _bulletBase;
        [SerializeField] private BulletOwner _bulletOwner;
        [SerializeField] private bool _enemyWeaponTracking;
        [Space]
        [SerializeField] private ProjectileComponent[] _bulletComponents;

        public ProjectileConfig Config => _bulletConfig;
        public BulletBase Base => _bulletBase;
        
        public ProjectileComponent[] BulletComponents => _bulletComponents;

        public BulletOwner BulletOwner => _bulletOwner;
        public bool IsTracking => _enemyWeaponTracking;
    }
}