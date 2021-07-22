using UnityEngine;
using UnityEngine.Animations;

namespace GrazingShmup
{
    public class PlayerShip : IMoveable
    {
        private PlayerData _playerData;
        private Transform _transform;
        private IEnginePlayer _movement;
        private IWeaponPlayer _weapon;
        private Transform _weaponMount;
        private PlayerPowerCore _powerCore;

        Animator[] _animators;
        ParticleSystem[] _particles;
        float[] _particleSpeed;

        public PlayerShip(PlayerData playerData, IEnginePlayer movement, IWeaponPlayer weapon)
        {
            _playerData = playerData;
            _movement = movement;
            _weapon = weapon;

            _transform = GameObject.Instantiate(_playerData.Prefab, _playerData.Position, Quaternion.identity).transform;

            _weaponMount = _transform.GetComponentInChildren<AimConstraint>().transform;

            _powerCore = new PlayerPowerCore(_transform);

            _movement.SetDependencies(_transform, _playerData);
            ServiceLocator.GetService<CollisionManager>().RegisterPlayer(_transform);
            ServiceLocator.GetService<CollisionManager>().PlayerHit += _powerCore.Hit;
            ServiceLocator.GetService<CollisionManager>().PlayerGrazed += _powerCore.Graze;

            _animators = _transform.GetComponentsInChildren<Animator>();
            _particles = _transform.GetComponentsInChildren<ParticleSystem>();
            _particleSpeed = new float[_particles.Length];

            for (int i = 0; i < _particles.Length; i++)
            {
                var main = _particles[i].main;

                _particleSpeed[i] = main.startSpeedMultiplier;
                main.startSpeedMultiplier = _particleSpeed[i] / 2;
            }
        }

        public Transform Transform => _transform;
        public IWeaponPlayer Weapon => _weapon;
        public IEnginePlayer Movement => _movement;

        public void Move(float inputHor, float inputVer, bool isSlowedDown, float deltaTime)
        {
            _movement.Move(inputHor, inputVer, isSlowedDown, deltaTime);
            Animate(inputHor, inputVer);

            _powerCore.Update(deltaTime);
        }

        public void Fire()
        {
            _weapon.Shoot(_weaponMount, _powerCore.PowerLevel);
        }

        private void Animate(float inputHor, float inputVer)
        {
            foreach (Animator animator in _animators)
            {
                if (inputHor > 0)
                {
                    animator.SetBool("IsMovingRight", true);
                    animator.SetBool("IsMovingLeft", false);
                }
                else if (inputHor < 0)
                {
                    animator.SetBool("IsMovingRight", false);
                    animator.SetBool("IsMovingLeft", true);
                }
                else
                {

                    animator.SetBool("IsMovingRight", false);
                    animator.SetBool("IsMovingLeft", false);
                }
            }

            for (int i = 0; i < _particles.Length; i++)
            {
                var main = _particles[i].main;

                if (inputVer > 0)
                    main.startSpeedMultiplier = _particleSpeed[i];
                else if (inputVer < 0)
                    main.startSpeedMultiplier = _particleSpeed[i] / 4;
                else
                    main.startSpeedMultiplier = _particleSpeed[i] / 2;
            }
        }

        public void SetMovement(IEnginePlayer movement)
        {
            _movement = movement;
        }

        public void SetWeapon(IWeaponPlayer weapon)
        {
            _weapon = weapon;
        }
    }
}