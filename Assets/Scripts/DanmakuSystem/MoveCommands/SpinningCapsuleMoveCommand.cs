using UnityEngine;


namespace GrazingShmup
{
    public class SpinningCapsuleMoveCommand : DelayedCapsuleMoveCommand
    {
        private float _nextfireDegree;
        private float _degreesSpun;
        private Quaternion _shootingRotation;

        public SpinningCapsuleMoveCommand(Transform projectile, BulletOwner owner, ProjectileConfig config, IProjectile content)
            : base(projectile, owner, config, content)
        {
            _speed = config.SpinningCapsuleSettings.SCapsuleSpeed;
            _deltaSpeed = config.SpinningCapsuleSettings.SCapsuleDeltaSpeed;
            _deltaSpeedDelay = config.SpinningCapsuleSettings.SCapsuleDeltaSpeedDelay;
            _turnSpeed = config.SpinningCapsuleSettings.SCapsuleTurnSpeed;

            _prefab = config.SpinningCapsuleSettings.SCapsulePrefab;

            _shootingRotation = _projectile.rotation;
        }

        public override bool Execute(float deltaTime)
        {
            SetDeltaTimeForurrentUpdate(deltaTime);

            SetLastUpdateTimeAndPosition();

            Move();

            CheckCollisions();

            if (!SpinAndShoot())
            {
                DisableProjectile();
                return false;
            }

            return true;
        }

        private bool SpinAndShoot()
        {

            if (_degreesSpun >= _nextfireDegree)
            {
                _content.Fire(_config, _projectile.position, _shootingRotation.eulerAngles);
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