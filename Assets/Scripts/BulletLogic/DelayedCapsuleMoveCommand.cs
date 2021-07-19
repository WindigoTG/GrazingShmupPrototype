using UnityEngine;

namespace GrazingShmup
{
    public class DelayedCapsuleMoveCommand : IBulletMoveCommand
    {
        private Transform _bullet;
        private float _speed;
        private float _angularSpeed;
        private float _delayTime;

        private Vector3 _lastPosition;
        private float _bulletRadius;

        private IFireable _content;
        private BullletConfig _config;

        public DelayedCapsuleMoveCommand(Transform bullet, float speed, float angularSpeed, float delayTime, IFireable content, BullletConfig config)
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

        public void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lastPosition = _bullet.position;

                _bullet.transform.Rotate(Vector3.forward, _angularSpeed * 180 / Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.up * _speed * deltaTime, Space.Self);

                ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(ConstantsAndMagicLines.PlayerLayer));


                _delayTime -= deltaTime;
                if (_delayTime <= 0)
                {
                    _content.Fire(_config, _bullet.position, _bullet.rotation.eulerAngles);
                    ServiceLocator.GetService<ObjectPoolManager>().EnemyBulletsPool.Push(_bullet.gameObject);
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