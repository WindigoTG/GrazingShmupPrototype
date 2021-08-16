using UnityEngine;

namespace GrazingShmup
{
    public class BaseProjectileMoveCommand : IProjectileMoveCommand
    {
        protected Transform _projectile;
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

        protected ObjectPoolManager _objectPoolManager;
        protected CollisionManager _collisionManager;

        public BaseProjectileMoveCommand(Transform projectile, BulletOwner owner, ProjectileConfig config)
        {
            _objectPoolManager = ServiceLocator.GetService<ObjectPoolManager>();
            _collisionManager = ServiceLocator.GetService<CollisionManager>();

            _projectile = projectile;
            _bulletOwner = owner;
            _speed = config.ProjectileSpeed;
            _deltaSpeed = config.ProjectileDeltaSpeed;
            _deltaSpeedDelay = config.ProjectileDeltaSpeedDelay;
            _turnSpeed = config.ProjectileTurnSpeed;
            _lifeTime = config.ProjectileLifeTime;
            _lastPosition = _projectile.position;
            GetBulletSize();
        }

        private void GetBulletSize()
        {
            var collider = _projectile.gameObject.AddComponent<BoxCollider2D>();
            _bulletRadius = collider.bounds.extents.x;
            Object.Destroy(collider);
        }

        public virtual bool Execute(float deltaTime)
        {
            SetDeltaTimeForurrentUpdate(deltaTime);

            SetLastUpdateTimeAndPosition();

            Move();

            if (CheckCollisions())
            {
                DisableProjectile();
                return false;
            }

            if (CheckIfLifeTimeIsOver())
            {
                DisableProjectile();
                return false;
            }

            return true;
        }

        protected void SetDeltaTimeForurrentUpdate(float deltaTime)
        {
            if (_lastUpdateTime != 0)
                _deltaTime = Time.time - _lastUpdateTime;
            else
                _deltaTime = deltaTime;
        }

        protected void SetLastUpdateTimeAndPosition()
        {
            _lastUpdateTime = Time.time;
            _lastPosition = _projectile.position;
        }

        protected bool CheckIfLifeTimeIsOver()
        {
            _lifeTime -= _deltaTime;

            if (_lifeTime <= 0)
                return true;
            else
                return false;
        }

        protected void Move()
        {
            //_bullet.transform.Rotate(Vector3.forward, _angularSpeed * _speed * _deltaTime);
            _projectile.transform.Rotate(Vector3.forward, _turnSpeed * 180 / Mathf.PI * _deltaTime);
            _projectile.Translate(Vector3.up * (_speed * _deltaTime), Space.Self);

            AdjustSpeed();
        }

        protected void AdjustSpeed()
        {
            if (_deltaSpeedDelay > 0)
                _deltaSpeedDelay -= _deltaTime;
            if (_deltaSpeedDelay <= 0)
                _speed += _deltaSpeed * _deltaTime;
        }

        protected bool CheckCollisions()
        {
            int layerMask = _bulletOwner == BulletOwner.Enemy ? LayerMask.GetMask(References.PlayerHitBox) : LayerMask.GetMask(References.EnemyLayer);

            return _collisionManager.CheckCollisions(_lastPosition, _bulletRadius, _projectile.position - _lastPosition, layerMask);
        }

        private void DisableProjectile()
        {
            if (_bulletOwner == BulletOwner.Enemy)
                _objectPoolManager.EnemyBulletsPool.Push(_projectile.gameObject);
            else
                _objectPoolManager.PlayerBulletsPool.Push(_projectile.gameObject);
        }
    }
}