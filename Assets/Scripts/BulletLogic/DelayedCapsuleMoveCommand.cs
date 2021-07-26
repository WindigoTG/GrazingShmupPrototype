using UnityEngine;

namespace GrazingShmup
{
    public class DelayedCapsuleMoveCommand : IBulletMoveCommand
    {
        protected Transform _bullet;
        protected float _speed;
        protected float _angularSpeed;
        protected float _delayTime;

        protected Vector3 _lastPosition;
        protected float _bulletRadius;

        protected IFireable _content;
        protected BulletConfig _config;

        public DelayedCapsuleMoveCommand(Transform bullet, float speed, float angularSpeed, float delayTime, IFireable content, BulletConfig config)
        {
            _bullet = bullet;
            _speed = speed;
            _angularSpeed = angularSpeed;
            _delayTime = delayTime;
            _content = content;
            _config = config;
            _lastPosition = _bullet.position;
            GetBulletSize();
        }

        public virtual void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lastPosition = _bullet.position;

                _bullet.transform.Rotate(Vector3.forward, _angularSpeed * 180 / Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.up * _speed * deltaTime, Space.Self);

                ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(References.PlayerHitBox));


                _delayTime -= deltaTime;
                if (_delayTime <= 0)
                {
                    _content.Fire(_config, _bullet.position, _bullet.rotation.eulerAngles);
                    ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Push(_bullet.gameObject);
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                }
            }
        }

        private void GetBulletSize()
        {
            var collider = _bullet.gameObject.AddComponent<BoxCollider2D>();
            _bulletRadius = collider.bounds.extents.x;
            Object.Destroy(collider);
        }
    }
}