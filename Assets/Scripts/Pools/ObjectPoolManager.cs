using UnityEngine;
using System.Collections.Generic;

namespace GrazingShmup
{
    internal sealed class ObjectPoolManager
    {
        private ObjectPool _playerBulletsPool;
        private ObjectPool _enemyBulletsPool;
        private ObjectPool _bulletCapsulePool;
        private ObjectPool _homingLaserPool;
        private Dictionary<EnemyType, ObjectPool> _enemyPool = new Dictionary<EnemyType, ObjectPool>();

        private Transform _playerBulletParent;
        private Transform _enemyBulletParent;
        private Transform _enemyParent;

        IEnemyFactory _enemyFactory;

        public ObjectPoolManager(IEnemyFactory enemyFactory)
        {
            _playerBulletParent = new GameObject(References.Player_Bullets_Parent_Object).transform;
            _enemyBulletParent = new GameObject(References.Enemy_Bullets_Parent_Object).transform;
            _enemyParent = new GameObject(References.Enemies_Parent_Object).transform;
            _enemyFactory = enemyFactory;
        }

        public ObjectPool PlayerBulletsPool
        {
            get
            {
                if (_playerBulletsPool == null)
                    _playerBulletsPool = new ObjectPool(Resources.Load<GameObject>(References.Player_Bullet_Prefab), _playerBulletParent);
                return _playerBulletsPool;
            }
        }

        public ObjectPool EnemyBulletsPool
        {
            get
            {
                if (_enemyBulletsPool == null)
                    _enemyBulletsPool = new ObjectPool(Resources.Load<GameObject>(References.Enemy_Bullet_Prefab), _enemyBulletParent);
                return _enemyBulletsPool;
            }
        }

        public ObjectPool BulletCapsulePool
        {
            get
            {
                if (_bulletCapsulePool == null)
                    _bulletCapsulePool = new ObjectPool(Resources.Load<GameObject>(References.Bullet_Capsule_Prefab), _enemyBulletParent);
                return _bulletCapsulePool;
            }
        }

        public ObjectPool HomingLaserPool
        {
            get
            {
                if (_homingLaserPool == null)
                    _homingLaserPool = new ObjectPool(Resources.Load<GameObject>(References.Homing_Laser), _enemyBulletParent);
                return _homingLaserPool;
            }
        }

        public ObjectPool GetEnemyPool(EnemyType type)
        {
            if (!_enemyPool.ContainsKey(type))
                _enemyPool.Add(type, new ObjectPool(_enemyFactory.GetEnemyPrefab(type), _enemyParent));

            return _enemyPool[type];
        }
    }
}