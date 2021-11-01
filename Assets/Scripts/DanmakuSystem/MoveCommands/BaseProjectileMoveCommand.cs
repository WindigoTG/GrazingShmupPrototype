using UnityEngine;
using Zenject;

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

        [Inject] protected ObjectPoolManager _objectPoolManager;
        [Inject] protected CollisionManager _collisionManager;

        protected GameObject _prefab;

        protected bool _shouldLiveOffscreen;
        protected float _offscreenOffset;

        public BaseProjectileMoveCommand(Transform projectile, BulletOwner owner, BulletConfig config)
        {
            _projectile = projectile;
            _bulletOwner = owner;
            _speed = config.BulletSpeed;
            _deltaSpeed = config.BulletDeltaSpeed;
            _deltaSpeedDelay = config.BulletDeltaSpeedDelay;
            _turnSpeed = config.BulletTurnSpeed;
            _lifeTime = config.BulletLifeTime;
            _lastPosition = _projectile.position;

            _prefab = config.BulletPrefab;

            _shouldLiveOffscreen = config.ShouldLiveOffscreen;
            _offscreenOffset = config.OffscreenOffset;

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

            if (CheckIfOutsideScreenBounds())
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
            _projectile.transform.Rotate(Vector3.forward, _turnSpeed * _deltaTime);
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

        protected bool CheckIfOutsideScreenBounds()
        {
            if (_shouldLiveOffscreen)
                return false;

            return !ScreenBounds.CheckIfWithinBounds(_projectile.position, _offscreenOffset);
        }

        protected void DisableProjectile()
        {
            _objectPoolManager.GetBulletPool(_prefab).Push(_projectile.gameObject);
        }
    }
}