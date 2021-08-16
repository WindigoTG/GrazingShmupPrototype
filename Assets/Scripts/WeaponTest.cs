using UnityEngine;

namespace GrazingShmup
{
    public class WeaponTest : MonoBehaviour
    {
        [SerializeField] private ProjectileConfig _config;
        [SerializeField] private BulletData _bulletData;
        [SerializeField] private Transform _testTarget;
        private BulletManager _bulletManager;
        private BulletFactory _bulletFactory;
        private IProjectile _projectile;
        private float _fireDelay;

        private float _lastFiredTime;

        private float _deltaTime;

        private void Awake()
        {
            _bulletFactory = new BulletFactory();

            CollisionManager collisionManager = new CollisionManager();
            float targetSize = _testTarget.GetComponent<CircleCollider2D>().radius;
            collisionManager.RegisterPlayer(_testTarget, targetSize, (0, 0));

            ServiceLocator.AddService(collisionManager);
            _bulletManager = new BulletManager();

            ServiceLocator.AddService(new ObjectPoolManager(null));
            ServiceLocator.AddService(_bulletManager);
            ServiceLocator.AddService(new PlayerTracker(_testTarget));

            _projectile = _bulletFactory.GetBullet(_bulletData.Base, _bulletData.BulletComponents, _bulletData.BulletOwner);
            _config = _bulletData.Config;
            _fireDelay = _config.FireDelay;
            _lastFiredTime = Time.time;
        }

        void Update()
        {
            if (Time.time - _lastFiredTime >= _fireDelay)
            {
                _projectile = _bulletFactory.GetBullet(_bulletData.Base, _bulletData.BulletComponents, _bulletData.BulletOwner);
                _config = _bulletData.Config;

                Fire();
                _lastFiredTime = Time.time;
                _fireDelay = _config.FireDelay;
            }
        }

        private void LateUpdate()
        {
            //if (Time.frameCount % 2 == 0)
            //    _bulletManager.LateUpdate(_deltaTime + Time.deltaTime);
            //else
            //    _deltaTime = Time.deltaTime;
            _bulletManager.LateUpdate(Time.deltaTime);
        }

        private void Fire()
        {
            _projectile.Fire(_config, transform.position, transform.rotation.eulerAngles);
        }
    }
}