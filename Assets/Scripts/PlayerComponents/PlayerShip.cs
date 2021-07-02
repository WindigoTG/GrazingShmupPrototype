using UnityEngine;

namespace GrazingShmup
{
    public class PlayerShip : IMoveable
    {
        private PlayerData _playerData;
        private Transform _transform;
        private IEnginePlayer _movement;
        private IWeaponPlayer _weapon;

        Animator[] _animators;
        ParticleSystem[] _particles;
        float[] _particleSpeed;

        public PlayerShip(PlayerData playerData, IEnginePlayer movement, IWeaponPlayer weapon)
        {
            _playerData = playerData;
            _movement = movement;
            _weapon = weapon;

            _transform = GameObject.Instantiate(_playerData.Prefab, _playerData.Position, Quaternion.identity).transform;

            _movement.SetDependencies(_transform, _playerData);
            //ServiceLocator.GetService<CollisionManager>().PlayerHit += _playerHealth.TakeHit;

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

        public void Move(float inputHor, float inputVer, float deltaTime)
        {
            _movement.Move(inputHor, inputVer, deltaTime);
            Animate(inputHor, inputVer);
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