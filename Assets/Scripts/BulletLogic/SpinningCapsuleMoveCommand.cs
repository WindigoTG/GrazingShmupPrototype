using UnityEngine;


namespace GrazingShmup
{
    public class SpinningCapsuleMoveCommand : DelayedCapsuleMoveCommand
    {
        private float _nextfireDegree;
        private float _degreesSpun;
        private Quaternion _shootingRotation;

        public SpinningCapsuleMoveCommand(Transform bullet, float speed, float deltaSpeed, float deltaSpeedDelay, float turnSpeed,
                                        float lifeTime, IProjectile content, ProjectileConfig config)
            : base(bullet, speed, deltaSpeed, deltaSpeedDelay, turnSpeed, lifeTime, content, config)
        {
            _config = config;
            _shootingRotation = _bullet.rotation;
        }

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

            if (!SpinAndShoot())
            {
                ServiceLocator.GetService<ObjectPoolManager>().BulletCapsulePool.Push(_bullet.gameObject);
                return false;
            }

            return true;
        }

        private bool SpinAndShoot()
        {

            if (_degreesSpun >= _nextfireDegree)
            {
                _content.Fire(_config, _bullet.position, _shootingRotation.eulerAngles);
                _nextfireDegree += _config.SpinningCapsuleSettings.DegreesBetweenShots;
            }

            float spinDegree = _config.SpinningCapsuleSettings.SpinSpeed * _deltaTime;
            _shootingRotation.eulerAngles = _shootingRotation.eulerAngles.Change(z: 
                                                                        _shootingRotation.eulerAngles.z + spinDegree);
            _degreesSpun += Mathf.Abs(spinDegree);

            if (_degreesSpun >= _config.SpinningCapsuleSettings.SpinArc)
                return false;

            return true;
        }
    }
}