using UnityEngine;

namespace GrazingShmup
{
    public class RepeaterCapsuleMoveCommand : DelayedCapsuleMoveCommand
    {
        float _refireTime;
        float _lastFireTime;

        public RepeaterCapsuleMoveCommand(Transform bullet, float speed, float deltaSpeed, float deltaSpeedDelay, float turnSpeed, 
                                        float lifeTime, IFireable content, ProjectileConfig config, float refireTime)  
            : base(bullet, speed, deltaSpeed, deltaSpeedDelay, turnSpeed, lifeTime, content, config)
        {
            _refireTime = refireTime;
            _lastFireTime = Time.time;
        }

        public override bool Execute(float deltaTime)
        {
            if (_lastUpdateTime != 0)
                _deltaTime = Time.time - _lastUpdateTime;
            else
                _deltaTime = deltaTime;

            _lastPosition = _bullet.position;

            _bullet.transform.Rotate(Vector3.forward, _angularSpeed * 180 / Mathf.PI * deltaTime);
            _bullet.Translate(Vector3.up * _speed * deltaTime, Space.Self);

            if (_deltaSpeedDelay > 0)
                _deltaSpeedDelay -= deltaTime;
            if (_deltaSpeedDelay <= 0)
                _speed += _deltaSpeed * deltaTime;

            ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(References.PlayerHitBox));

            if (Time.time - _lastFireTime >= _refireTime)
            {
                _content.Fire(_config, _bullet.position, _bullet.rotation.eulerAngles);
                _lastFireTime = Time.time;
            }

            _lifeTime -= deltaTime;
            if (_lifeTime <= 0)
            {
                ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Push(_bullet.gameObject);
                _lastUpdateTime = Time.time;
                return false;
            }

            _lastUpdateTime = Time.time;
            return true;
        }
    }
}