using UnityEngine;

namespace GrazingShmup
{
    public class WeaponTest : MonoBehaviour
    {
        #region Fields

        [SerializeField] private BulletConfig _config;
        [SerializeField] private BulletData _bulletData;
        [SerializeField] private Transform _testTarget;
        private BulletManager _bulletManager;
        private BulletFactory _bulletFactory;
        private IProjectile _projectile;
        private float _fireDelay;

        private float _lastFiredTime;

        private float _deltaTime;

        #endregion


        #region UnityMethods

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

            _projectile = _bulletFactory.GetBullet(_bulletData.BaseBullet, _bulletData.BulletComponents, _bulletData.BulletOwner);
            _config = _bulletData.BulletConfig;
            _fireDelay = _config.FireDelay;
            _lastFiredTime = Time.time;
        }

        void Update()
        {
            if (Time.time - _lastFiredTime >= _fireDelay)
            {
                _projectile = _bulletFactory.GetBullet(_bulletData.BaseBullet, _bulletData.BulletComponents, _bulletData.BulletOwner);
                _config = _bulletData.BulletConfig;

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
            _bulletManager.UpdateLate(Time.deltaTime);
        }

        #endregion


        #region Methods

        private void Fire()
        {
            _projectile.Fire(_config, transform.position, transform.rotation.eulerAngles);
        }

        #endregion
    }
}