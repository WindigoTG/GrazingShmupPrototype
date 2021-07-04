using UnityEngine;

namespace GrazingShmup
{
    public class PlayerBulletMoveCommand : IBulletMoveCommand
    {
        private Transform _bullet;
        private float _speed;
        private float _angularSpeed;
        private float _lifeTime;

        private Vector3 _lastPosition;
        private float _bulletRadius;

        public PlayerBulletMoveCommand(Transform bullet, float speed, float angularSpeed, float lifeTime)
        {
            _bullet = bullet;
            _speed = speed;
            _angularSpeed = angularSpeed;
            _lifeTime = lifeTime;
            _lastPosition = _bullet.position;
            GetBulletSize();
        }

        public void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lastPosition = _bullet.position;

                _lifeTime -= deltaTime;
                if (_lifeTime <= 0)
                {
                    ServiceLocator.GetService<ObjectPoolManager>().PlayerBulletsPool.Push(_bullet.gameObject);
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                }

                _bullet.transform.Rotate(Vector3.forward, _angularSpeed * 180 / Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.up * _speed * deltaTime, Space.Self);

                if (ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(ConstantsAndMagicLines.EnemyLayer)))
                {
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                    ServiceLocator.GetService<ObjectPoolManager>().PlayerBulletsPool.Push(_bullet.gameObject);
                }
            }
            else
                ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
        }

        private void GetBulletSize()
        {
            var collider =_bullet.gameObject.AddComponent<BoxCollider>();
            _bulletRadius = collider.bounds.extents.z;
            Object.Destroy(collider);
        }
    }
}