using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace GrazingShmup
{
    public sealed class ObjectPoolManager
    {
        private Dictionary<EnemyType, ObjectPool> _enemyPool = new Dictionary<EnemyType, ObjectPool>();
        private Dictionary<GameObject, ObjectPool> _bulletsPool = new Dictionary<GameObject, ObjectPool>();

        private Transform _enemyParent;
        private Transform _bulletsParent;

        [Inject] IEnemyFactory _enemyFactory;

        public ObjectPoolManager()
        {
            _enemyParent = new GameObject(References.Enemies_Parent_Object).transform;
            _bulletsParent = new GameObject(References.Bullets_Parent_Object).transform;
        }

        public ObjectPool GetEnemyPool(EnemyType type)
        {
            if (!_enemyPool.ContainsKey(type))
                _enemyPool.Add(type, new ObjectPool(_enemyFactory.GetEnemyPrefab(type), _enemyParent));

            return _enemyPool[type];
        }

        public ObjectPool GetBulletPool(GameObject prefab)
        {
            if (!_bulletsPool.ContainsKey(prefab))
                _bulletsPool.Add(prefab, new ObjectPool(prefab, _bulletsParent));

            return _bulletsPool[prefab];
        }
    }
}