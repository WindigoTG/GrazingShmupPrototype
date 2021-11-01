using UnityEngine;

namespace GrazingShmup
{
    public class RepeaterCapsuleMoveCommand : DelayedCapsuleMoveCommand
    {
        protected float _refireTime;
        protected float _lastFireTime;

        public RepeaterCapsuleMoveCommand(Transform projectile, BulletOwner owner, BulletConfig config, IProjectile content)  
            : base(projectile, owner, config, content)
        {
            _speed = config.RepeaterCapsuleSettings.RCapsuleSpeed;
            _deltaSpeed = config.RepeaterCapsuleSettings.RCapsuleDeltaSpeed;
            _deltaSpeedDelay = config.RepeaterCapsuleSettings.RCapsuleDeltaSpeedDelay;
            _turnSpeed = config.RepeaterCapsuleSettings.RCapsuleTurnSpeed;
            _lifeTime = config.RepeaterCapsuleSettings.RCapsuleLifeTime;

            _prefab = config.RepeaterCapsuleSettings.RCapsulePrefab;

            _refireTime = config.RepeaterCapsuleSettings.RCapsuleRefireTime;
            _lastFireTime = Time.time;
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
                if (_config.RepeaterCapsuleSettings.SynchronizeDeltaSpeedDelayInShots)
                    _config.ModifyDeltaSpeedDelay(-_config.RepeaterCapsuleSettings.RCapsuleRefireTime);

                _content.Fire(_config, _projectile.position, _projectile.rotation.eulerAngles);
                _lastFireTime = Time.time;
            }
        }
    }
}