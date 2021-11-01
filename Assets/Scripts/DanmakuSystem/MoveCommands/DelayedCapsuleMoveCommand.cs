using UnityEngine;

namespace GrazingShmup
{
    public class DelayedCapsuleMoveCommand : BaseProjectileMoveCommand
    {
        protected IProjectile _content;
        protected BulletConfig _config;

        public DelayedCapsuleMoveCommand(Transform projectile, BulletOwner owner, BulletConfig config, IProjectile content)
            : base(projectile, owner, config)
        {
            _speed = config.CapsuleSettings.CapsuleSpeed;
            _deltaSpeed = config.CapsuleSettings.CapsuleDeltaSpeed;
            _deltaSpeedDelay = config.CapsuleSettings.CapsuleDeltaSpeedDelay;
            _turnSpeed = config.CapsuleSettings.CapsuleTurnSpeed;
            _lifeTime = config.CapsuleSettings.CapsuleLifeTime;

            _prefab = config.CapsuleSettings.CapsulePrefab;

            _content = content;
            _config = config;
        }

        public override bool Execute(float deltaTime)
        {
            SetDeltaTimeForurrentUpdate(deltaTime);

            SetLastUpdateTimeAndPosition();

            Move();

            CheckCollisions();

            if (CheckIfLifeTimeIsOver())
            {
                _content.Fire(_config, _projectile.position, _projectile.rotation.eulerAngles);
                DisableProjectile();
                return false;
            }

            if (CheckIfOutsideScreenBounds())
            {
                _content.Fire(_config, _projectile.position, _projectile.rotation.eulerAngles);
                DisableProjectile();
                return false;
            }

            return true;
        }
    }
}