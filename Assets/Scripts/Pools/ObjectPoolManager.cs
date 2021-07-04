using UnityEngine;

namespace GrazingShmup
{
    internal sealed class ObjectPoolManager
    {
        private ObjectPool _playerBulletsPool;
        private ObjectPool _enemyBulletsPool;
        private ObjectPool _enemyPool;

        private Transform _playerBulletParent;
        private Transform _enemyBulletParent;
        private Transform _enemyParent;

        public ObjectPoolManager()
        {
            _playerBulletParent = new GameObject(ConstantsAndMagicLines.Player_Bullets_Parent_Object).transform;
            _enemyBulletParent = new GameObject(ConstantsAndMagicLines.Enemy_Bullets_Parent_Object).transform;
            _enemyParent = new GameObject(ConstantsAndMagicLines.Enemies_Parent_Object).transform;
        }

        public ObjectPool PlayerBulletsPool
        {
            get
            {
                if (_playerBulletsPool == null)
                    _playerBulletsPool = new ObjectPool(Resources.Load<GameObject>(ConstantsAndMagicLines.Player_Bullet_Prefab), _playerBulletParent);
                return _playerBulletsPool;
            }
        }

        public ObjectPool EnemyBulletsPool
        {
            get
            {
                if (_enemyBulletsPool == null) 
                    _enemyBulletsPool = new ObjectPool(Resources.Load<GameObject>(ConstantsAndMagicLines.Enemy_Bullet_Prefab), _enemyBulletParent);
                return _enemyBulletsPool;
            }
        }

        //public ObjectPool EnemyPool
        //{
        //    get
        //    {
        //        if (_enemyPool == null)
        //            _enemyPool = new ObjectPool(Resources.Load<GameObject>(ConstantsAndMagicLines.Enemy_Prefab), _enemyParent);
        //        return _enemyPool;
        //    }
        //}
    }
}