using UnityEngine;

namespace GrazingShmup
{ 
public class BurstCapsuleMoveCommand : RepeaterCapsuleMoveCommand
    { 
        public BurstCapsuleMoveCommand(Transform projectile, BulletOwner owner, BulletConfig config, IProjectile content)
            : base (projectile, owner, config, content) 
        {
            _speed = config.BurstCapsuleSettings.BCapsuleSpeed;
            _deltaSpeed = config.BurstCapsuleSettings.BCapsuleDeltaSpeed;
            _deltaSpeedDelay = config.BurstCapsuleSettings.BCapsuleDeltaSpeedDelay;
            _turnSpeed = config.BurstCapsuleSettings.BCapsuleTurnSpeed;
            _lifeTime = config.BurstCapsuleSettings.BCapsuleLifeTime;

            _prefab = config.BurstCapsuleSettings.BCapsulePrefab;

            _refireTime = config.BurstCapsuleSettings.BurstRefireTime;
        }

        public override bool Execute(float deltaTime)
        {
            SetDeltaTimeForurrentUpdate(deltaTime);

            SetLastUpdateTimeAndPosition();

            Move();

            CheckCollisions();

            FireContent();

            if (CheckIfLifeTimeIsOver())
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

        private void FireContent()
        {
            if (Time.time - _lastFireTime >= _refireTime)
            {
                Vector3 rotation;

                if (_config.BurstCapsuleSettings.IsTracking)
                {
                    Quaternion newRotation = new Quaternion();
                    Vector3 targetPosition = ServiceLocator.GetService<PlayerTracker>().Player.position;
                    newRotation.SetLookRotation(Vector3.forward, targetPosition - _projectile.position);

                    rotation = newRotation.eulerAngles;
                }
                else
                    rotation = _projectile.rotation.eulerAngles;

                _content.Fire(_config, _projectile.position, rotation);
                _lastFireTime = Time.time;

                _config.ModifySpeed(_config.BurstCapsuleSettings.DeltaSpeedInBurst);
            }
        }
    }
}