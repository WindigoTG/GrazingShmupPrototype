using UnityEngine;

namespace GrazingShmup
{
    public class BaseBulletMoveCommand : IBulletMoveCommand
    {
        protected Transform _bullet;
        protected float _speed;
        protected float _deltaSpeed;
        protected float _deltaSpeedDelay;
        protected float _turnSpeed;
        protected float _lifeTime;

        protected Vector3 _lastPosition;
        protected float _bulletRadius;

        protected BulletOwner _bulletOwner;

        protected float _lastUpdateTime;
        protected float _deltaTime;

        public BaseBulletMoveCommand(Transform bullet, BulletOwner owner, float speed, float deltaSpeed, float deltaSpeedDelay, float turnSpeed, float lifeTime)
        {
            _bullet = bullet;
            _bulletOwner = owner;
            _speed = speed;
            _deltaSpeed = deltaSpeed;
            _deltaSpeedDelay = deltaSpeedDelay;
            _turnSpeed = turnSpeed;
            _lifeTime = lifeTime;
            _lastPosition = _bullet.position;
            GetBulletSize();
        }

        public virtual bool Execute(float deltaTime)
        {
            if (_lastUpdateTime != 0)
                _deltaTime = Time.time - _lastUpdateTime;
            else
                _deltaTime = deltaTime;

            _lastPosition = _bullet.position;

            _lifeTime -= _deltaTime;
            if (_lifeTime <= 0)
            {
                DisableBullet();
                _lastUpdateTime = Time.time;
                return false;
            }

            //_bullet.transform.Rotate(Vector3.forward, _angularSpeed * _speed * _deltaTime);
            _bullet.transform.Rotate(Vector3.forward, _turnSpeed * 180 / Mathf.PI * _deltaTime);
            _bullet.Translate(Vector3.up * (_speed * _deltaTime), Space.Self);

            if (_deltaSpeedDelay > 0)
                _deltaSpeedDelay -= _deltaTime;
            if (_deltaSpeedDelay <= 0)
                _speed += _deltaSpeed * _deltaTime;

            int layerMask = _bulletOwner == BulletOwner.Enemy ? LayerMask.GetMask(References.PlayerHitBox) : LayerMask.GetMask(References.EnemyLayer);

            if (ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                _lastPosition, _bulletRadius, _bullet.position - _lastPosition, layerMask))
            {
                DisableBullet();
                _lastUpdateTime = Time.time;
                return false;
            }

            _lastUpdateTime = Time.time;
            return true;
        }

        private void DisableBullet()
        {
            if (_bulletOwner == BulletOwner.Enemy)
                ServiceLocator.GetService<ObjectPoolManager>().EnemyBulletsPool.Push(_bullet.gameObject);
            else
                ServiceLocator.GetService<ObjectPoolManager>().PlayerBulletsPool.Push(_bullet.gameObject);
        }

        private void GetBulletSize()
        {
            var collider = _bullet.gameObject.AddComponent<BoxCollider2D>();
            _bulletRadius = collider.bounds.extents.x;
            Object.Destroy(collider);
        }
    }
}