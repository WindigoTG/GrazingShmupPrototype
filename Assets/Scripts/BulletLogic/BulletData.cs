using UnityEngine;

namespace GrazingShmup
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public sealed class BulletData : ScriptableObject
    {
        [SerializeField] BulletConfig _bulletConfig;
        [Space]
        [SerializeField] private BulletOwner _bulletOwner;
        [Space]
        [SerializeField] private BulletComponent[] _bulletComponents;

        public BulletConfig Config => _bulletConfig;
        
        public BulletComponent[] BulletComponents => _bulletComponents;

        public BulletOwner BulletOwner => _bulletOwner;
    }
}