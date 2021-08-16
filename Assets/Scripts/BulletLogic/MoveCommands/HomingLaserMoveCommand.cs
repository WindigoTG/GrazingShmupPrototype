using UnityEngine;

namespace GrazingShmup
{
    public class HomingLaserMoveCommand : BaseProjectileMoveCommand
    {
        float _homingTime;
        float _homingSpeed;
        Transform _target;
        Quaternion _rotation;

        public HomingLaserMoveCommand(Transform projectile, BulletOwner owner, ProjectileConfig config) : base(projectile, owner, config)
        {
            _homingSpeed = config.ProjectileTurnSpeed;
            _homingTime = config.HomingTime;

            _target = ServiceLocator.GetService<PlayerTracker>().Player;
        }

        public override bool Execute(float deltaTime)
        {
            SetDeltaTimeForurrentUpdate(deltaTime);

            SetLastUpdateTimeAndPosition();

            Move();

            if (CheckIfLifeTimeIsOver())
            {
                DisableProjectile();
                return false;
            }

            if (CheckCollisions())
            {
                _homingTime = 0;
            }
            return true;
        }

        new private void Move()
        {
            if (_homingTime > 0)
            {
                TrackTarget();
            }

            _projectile.Translate(Vector3.up * (_speed * _deltaTime), Space.Self);

            AdjustSpeed();
        }

        private void TrackTarget()
        {
            var dir = _target.position - _projectile.position;
            var angle = Vector3.Angle(Vector3.up, dir);
            var axis = Vector3.Cross(Vector3.up, dir);

            _rotation = Quaternion.RotateTowards(_projectile.rotation,
                                                Quaternion.AngleAxis(angle, axis),
                                                _homingSpeed * 180 / Mathf.PI * _deltaTime);
            _projectile.rotation = _rotation;

            _homingTime -= _deltaTime;
        }

        private void DisableProjectile()
        {
            _objectPoolManager.HomingLaserPool.Push(_projectile.gameObject);
        }
    }
}