using UnityEngine;

namespace GrazingShmup
{ 
public class BurstCapsuleMoveCommand : RepeaterCapsuleMoveCommand
    { 
        public BurstCapsuleMoveCommand(Transform bullet, float speed, float deltaSpeed, float deltaSpeedDelay, float turnSpeed,
                                        float lifeTime, IProjectile content, ProjectileConfig config, float refireTime)
            : base (bullet, speed, deltaSpeed, deltaSpeedDelay, turnSpeed, lifeTime, content, config, refireTime) { }

        public override bool Execute(float deltaTime)
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

            if (Time.time - _lastFireTime >= _refireTime)
            {
                Vector3 rotation;

                if (_config.BurstCapsuleSettings.IsTracking)
                {
                    Quaternion newRotation = new Quaternion();
                    Vector3 targetPosition = ServiceLocator.GetService<PlayerTracker>().Player.position;
                    newRotation.SetLookRotation(Vector3.forward, targetPosition - _bullet.position);

                    rotation = newRotation.eulerAngles;
                }
                else
                    rotation = _bullet.rotation.eulerAngles;

                _content.Fire(_config, _bullet.position, rotation);
                _lastFireTime = Time.time;

                _config.ModifySpeed(_config.BurstCapsuleSettings.DeltaSpeedInBurst);
            }

            _lifeTime -= deltaTime;
            if (_lifeTime <= 0)
            {
                ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Push(_bullet.gameObject);
                return false;
            }

            return true;
        }
    }
}