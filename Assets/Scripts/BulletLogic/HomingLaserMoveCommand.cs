using UnityEngine;

namespace GrazingShmup
{
    public class HomingLaserMoveCommand : BaseBulletMoveCommand
    {
        float _homingTime;
        float _homingSpeed;
        Transform _target;
        Quaternion _rotation;

        public HomingLaserMoveCommand(Transform bullet, BulletOwner owner, float speed, float deltaSpeed, float deltaSpeedDelay, float turnSpeed, float homingTime, float lifeTime) :
            base(bullet, owner, speed, deltaSpeed, deltaSpeedDelay, turnSpeed, lifeTime)
        {
            _homingSpeed = turnSpeed;
            _homingTime = homingTime;

            _target = ServiceLocator.GetService<PlayerTracker>().Player;
        }

        public override void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lastPosition = _bullet.position;

                _lifeTime -= deltaTime;
                if (_lifeTime <= 0)
                    DisableBullet();

                if (_homingTime > 0)
                {
                    TrackTarget(deltaTime);
                    _homingTime -= deltaTime;
                }

                _bullet.Translate(Vector3.up * (_speed * deltaTime), Space.Self);
                _speed += _deltaSpeed * deltaTime;

                int layerMask = LayerMask.GetMask(References.PlayerHitBox);

                if (ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, layerMask))
                {
                    //DisableBullet();
                    _homingTime = 0;
                }
            }
            else
                ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
        }

        private void DisableBullet()
        {
            ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
            ServiceLocator.GetService<ObjectPoolManager>().HomingLaserPool.Push(_bullet.gameObject);
        }

        private void TrackTarget(float deltaTime)
        {
            var dir = _target.position - _bullet.position;
            var angle = Vector3.Angle(Vector3.up, dir);
            var axis = Vector3.Cross(Vector3.up, dir);

            _rotation = Quaternion.RotateTowards(_bullet.rotation,
                                                Quaternion.AngleAxis(angle, axis),
                                                _homingSpeed * 180 / Mathf.PI * deltaTime);
            _bullet.rotation = _rotation;
        }
    }
}