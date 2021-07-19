using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class WeaponTest : MonoBehaviour
    {
        [SerializeField] private BullletConfig _config;
        [SerializeField] private BulletData _bulletData;
        private BulletManager _bulletManager;
        private IFireable _projectile;
        private float _fireDelay;

        private float _lastFiredTime;

        private float _deltaTime;

        private void Awake()
        {
            ServiceLocator.AddService(new CollisionManager());
            _bulletManager = new BulletManager();

            ServiceLocator.AddService(new ObjectPoolManager());
            ServiceLocator.AddService(_bulletManager);

            //_projectile = new SingleBullet(BulletOwner.Player).FiredInRow();
            //_projectile = new SingleBullet(BulletOwner.Player).FiredInArc();
            //_projectile = new SingleBullet(BulletOwner.Player).FiredInRow().FiredInArc().FiredInLine();
            _projectile = new SingleBullet(BulletOwner.Player).FiredInRow().FiredInArc().FiredInLine().FiredInDelayedCapsule();

            _config = _bulletData.GetConfig();
            _fireDelay = _config.FireDelay;
            _lastFiredTime = Time.time;
        }

        void Update()
        {
            if (Time.time - _lastFiredTime >= _fireDelay)
            {
                Fire();
                _config = _bulletData.GetConfig();
                _lastFiredTime = Time.time;
                _fireDelay = _config.FireDelay;
            }
        }

        private void LateUpdate()
        {
            if (Time.frameCount % 2 == 0)
                _bulletManager.LateUpdate(_deltaTime + Time.deltaTime);
            else
                _deltaTime = Time.deltaTime;
            //_bulletManager.LateUpdate(Time.deltaTime);
        }

        private void Fire()
        {
            _projectile.Fire(_config, transform.position, transform.rotation.eulerAngles);
        }
    }
}