using System;
using UnityEngine;

namespace GrazingShmup
{
    public sealed class Enemy : IUpdateableRegular
    {
        #region Fields

        private IMovementEnemy _movement;
        private IWeaponEnemy _weapon;
        private Transform _enemy;
        private Transform _weaponMount;
        EnemyType _type;

        public Action<Enemy> Deactivation;

        private ObjectPool _enemyPool;
        private PlayerController _playerController;

        #endregion


        #region Properties

        public bool IsActive => _enemy != null;

        #endregion


        #region ClassLifeCycles

        public Enemy(EnemyType type, IWeaponEnemy weapon, IMovementEnemy movement)
        {
            _weapon = weapon;
            _movement = movement;
            _enemyPool = ServiceLocator.GetService<ObjectPoolManager>().GetEnemyPool(_type);
        }

        #endregion


        #region IUpdateableRegular

        public void UpdateRegular(float deltaTime)
        {
            _movement?.Move(deltaTime);
            Shoot();
        }

        #endregion


        #region Methods

        public void ActivateEnemy(Vector3[] route, PlayerController playerController)
        {
            _enemy = _enemyPool.Pop().transform;
            _weaponMount = _enemy.GetComponentInChildren<Grid>().transform;
            _enemy.position = route[0];
            _movement.SetObjectToMoveAndRoute(_enemy, route);
            _playerController = playerController;
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

        #endregion
    }
}
