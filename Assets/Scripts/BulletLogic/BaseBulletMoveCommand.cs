using UnityEngine;

namespace GrazingShmup
{
    public class BaseBulletMoveCommand : IBulletMoveCommand
    {
        protected Transform _bullet;
        protected float _speed;
        protected float _deltaSpeed;
        protected float _angularSpeed;
        protected float _lifeTime;

        protected Vector3 _lastPosition;
        protected float _bulletRadius;

        protected BulletOwner _bulletOwner;

        public BaseBulletMoveCommand(Transform bullet, BulletOwner owner, float speed, float deltaSpeed, float angularSpeed, float lifeTime)
        {
            _bullet = bullet;
            _bulletOwner = owner;
            _speed = speed;
            _deltaSpeed = deltaSpeed;
            _angularSpeed = angularSpeed;
            _lifeTime = lifeTime;
            _lastPosition = _bullet.position;
            GetBulletSize();
        }

        public virtual void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lastPosition = _bullet.position;

                _lifeTime -= deltaTime;
                if (_lifeTime <= 0)
                    DisableBullet();

                //_bullet.transform.Rotate(Vector3.forward, _angularSpeed * _speed * deltaTime);
                _bullet.transform.Rotate(Vector3.forward, _angularSpeed * 180 / Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.up * (_speed * deltaTime), Space.Self);
                _speed += _deltaSpeed * deltaTime;

                int layerMask = _bulletOwner == BulletOwner.Enemy ? LayerMask.GetMask(References.PlayerHitBox) : LayerMask.GetMask(References.EnemyLayer);

                if (ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, layerMask))
                {
                    DisableBullet();
                }
            }
            else
                ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
        }

        private void DisableBullet()
        {
            ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
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