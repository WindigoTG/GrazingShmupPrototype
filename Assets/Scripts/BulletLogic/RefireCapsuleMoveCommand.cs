using UnityEngine;

namespace GrazingShmup
{
    public class RefireCapsuleMoveCommand : DelayedCapsuleMoveCommand
    {
        float _refireTime;
        float _lastFireTime;

        public RefireCapsuleMoveCommand(Transform bullet, float speed, float angularSpeed, float delayTime, IFireable content, BulletConfig config, float refireTime)  
            : base(bullet, speed, angularSpeed, delayTime, content, config)
        {
            _refireTime = refireTime;
            _lastFireTime = Time.time;
        }

        public override void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lastPosition = _bullet.position;

                _bullet.transform.Rotate(Vector3.forward, _angularSpeed * 180 / Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.up * _speed * deltaTime, Space.Self);

                ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(References.PlayerHitBox));

                if (Time.time - _lastFireTime >= _refireTime)
                {
                    _content.Fire(_config, _bullet.position, _bullet.rotation.eulerAngles);
                    _lastFireTime = Time.time;
                }

                _delayTime -= deltaTime;
                if (_delayTime <= 0)
                {
                    ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Push(_bullet.gameObject);
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                }
            }
        }
    }
}