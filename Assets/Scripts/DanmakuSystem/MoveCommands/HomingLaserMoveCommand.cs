using UnityEngine;
using Zenject;

namespace GrazingShmup
{
    public class HomingLaserMoveCommand : BaseProjectileMoveCommand
    {
        float _homingTime;
        Transform _target;
        Quaternion _rotation;

        [Inject] PlayerTracker _playerTracker;

        public HomingLaserMoveCommand(Transform projectile, BulletOwner owner, BulletConfig config) : base(projectile, owner, config)
        {
            _homingTime = config.HomingTime;

            _prefab = config.HomingLaserPrefab;

            _target = _playerTracker.Player;
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

            if (CheckIfOutsideScreenBounds())
            {
                DisableProjectile();
                return false;
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
                                                _turnSpeed * 180 / Mathf.PI * _deltaTime);
            _projectile.rotation = _rotation;

            _homingTime -= _deltaTime;
        }
    }
}