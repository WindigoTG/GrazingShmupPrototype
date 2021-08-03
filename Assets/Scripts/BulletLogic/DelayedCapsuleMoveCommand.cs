using UnityEngine;

namespace GrazingShmup
{
    public class DelayedCapsuleMoveCommand : IBulletMoveCommand
    {
        protected Transform _bullet;
        protected float _speed;
        protected float _deltaSpeed;
        protected float _deltaSpeedDelay;
        protected float _angularSpeed;
        protected float _lifeTime;

        protected Vector3 _lastPosition;
        protected float _bulletRadius;

        protected IProjectile _content;
        protected ProjectileConfig _config;

        protected float _lastUpdateTime;
        protected float _deltaTime;

        public DelayedCapsuleMoveCommand(Transform bullet, float speed, float deltaSpeed, float deltaSpeedDelay, float angularSpeed, float lifeTime, IProjectile content, ProjectileConfig config)
        {
            _bullet = bullet;
            _speed = speed;
            _angularSpeed = angularSpeed;
            _deltaSpeed = deltaSpeed;
            _deltaSpeedDelay = deltaSpeedDelay;
            _lifeTime = lifeTime;
            _content = content;
            _config = config;
            _lastPosition = _bullet.position;
            GetBulletSize();
        }

        public virtual bool Execute(float deltaTime)
        {
            if (_lastUpdateTime != 0)
                _deltaTime = Time.time - _lastUpdateTime;
            else
                _deltaTime = deltaTime;

            _lastUpdateTime = Time.time;
            _lastPosition = _bullet.position;

            _bullet.transform.Rotate(Vector3.forward, _angularSpeed * 180 / Mathf.PI * deltaTime);
            _bullet.Translate(Vector3.up * _speed * deltaTime, Space.Self);

            if (_deltaSpeedDelay > 0)
                _deltaSpeedDelay -= deltaTime;
            if (_deltaSpeedDelay <= 0)
                _speed += _deltaSpeed * deltaTime;

            ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(References.PlayerHitBox));


            _lifeTime -= deltaTime;
            if (_lifeTime <= 0)
            {
                _content.Fire(_config, _bullet.position, _bullet.rotation.eulerAngles);
                ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Push(_bullet.gameObject);
                return false;
            }

            return true;
        }

        private void GetBulletSize()
        {
            var collider = _bullet.gameObject.AddComponent<BoxCollider2D>();
            _bulletRadius = collider.bounds.extents.x;
            Object.Destroy(collider);
        }
    }
}