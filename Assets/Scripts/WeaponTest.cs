using UnityEngine;
using Zenject;

namespace GrazingShmup
{
    public class WeaponTest : MonoBehaviour
    {
        [SerializeField] private BulletConfig _config;
        [SerializeField] private BulletData _bulletData;
        [SerializeField] private Transform _testTarget;
        [Inject] private BulletManager _bulletManager;
        [Inject] private IBulletFactory _bulletFactory;
        [Inject] private PlayerTracker _playerTracker;
        [Inject] private CollisionManager _collisionManager;
        private IProjectile _projectile;
        private float _fireDelay;

        private float _lastFiredTime;

        private float _deltaTime;

        private void Awake()
        {
            float targetSize = _testTarget.GetComponent<CircleCollider2D>().radius;
            _collisionManager.RegisterPlayer(_testTarget, targetSize, (0, 0));

            _playerTracker.UpdatePlayer(_testTarget);

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
            _bulletManager.LateUpdate(Time.deltaTime);
        }

        private void Fire()
        {
            _projectile.Fire(_config, transform.position, transform.rotation.eulerAngles);
        }
    }
}