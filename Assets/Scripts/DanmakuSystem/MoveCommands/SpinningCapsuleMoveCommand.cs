using UnityEngine;


namespace GrazingShmup
{
    public class SpinningCapsuleMoveCommand : DelayedCapsuleMoveCommand
    {
        #region Fields

        private float _nextfireDegree;
        private float _degreesSpun;
        private Quaternion _shootingRotation;

        #endregion


        #region ClassLifeCycles

        public SpinningCapsuleMoveCommand(Transform projectile, BulletOwner owner, BulletConfig config, IProjectile content)
            : base(projectile, owner, config, content)
        {
            _speed = config.SpinningCapsuleSettings.SCapsuleSpeed;
            _deltaSpeed = config.SpinningCapsuleSettings.SCapsuleDeltaSpeed;
            _deltaSpeedDelay = config.SpinningCapsuleSettings.SCapsuleDeltaSpeedDelay;
            _turnSpeed = config.SpinningCapsuleSettings.SCapsuleTurnSpeed;

            _prefab = config.SpinningCapsuleSettings.SCapsulePrefab;

            _shootingRotation = _projectile.rotation;
        }

        #endregion


        #region IProjectileMoveCommand

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

            if (CheckIfOutsideScreenBounds())
            {
                DisableProjectile();
                return false;
            }

            return true;
        }

        #endregion


        #region Methods

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

        #endregion
    }
}