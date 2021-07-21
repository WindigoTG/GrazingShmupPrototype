using System;
using UnityEngine;

namespace GrazingShmup
{
    public enum EnemyType
    {
        PopcornFlying = 0
    }

    public sealed class Enemy : IUpdateableRegular
    {
        private IMovementEnemy _movement;
        private IWeaponEnemy _weapon;
        private Transform _enemy;
        private Transform _weaponMount;
        EnemyType _type;

        public Action<Enemy> Deactivation;

        private ObjectPool _enemyPool;
        private PlayerController _playerController;

        public Enemy(EnemyType type, IWeaponEnemy weapon, IMovementEnemy movement)
        {
            _weapon = weapon;
            _movement = movement;
            _enemyPool = ServiceLocator.GetService<ObjectPoolManager>().GetEnemyPool(_type);
        }

        public bool IsActive => _enemy != null;

        public void ActivateEnemy(Vector3[] route, PlayerController playerController)
        {
            _enemy = _enemyPool.Pop().transform;
            _weaponMount = _enemy.GetComponentInChildren<Grid>().transform;
            _enemy.position = route[0];
            _movement.SetObjectToMoveAndRoute(_enemy, route);
            _playerController = playerController;
        }

        public void Update(float deltaTime)
        {
            _movement?.Move(deltaTime);
            Shoot();
        }

        private void Shoot()
        {
            if (_playerController.PlayerPosition != null)
                _weapon.Shoot(_weaponMount, _playerController.PlayerPosition);
            else
                _weapon.Shoot(_weaponMount, _enemy.up);
        }

        private void CheckHit(Transform hit)
        {
            Transform[] parts = _enemy.GetComponentsInChildren<Transform>();
            foreach (Transform t in parts)
                if (t == hit)
                {
                    DeactivateEnemy();
                    break;
                }
        }

        private void DeactivateEnemy()
        {
            ServiceLocator.GetService<CollisionManager>().EnemyHit -= CheckHit;
            Deactivation?.Invoke(this);
            _enemyPool.Push(_enemy.gameObject);
            _enemy = null;
            _weaponMount = null;
        }
    }
}
