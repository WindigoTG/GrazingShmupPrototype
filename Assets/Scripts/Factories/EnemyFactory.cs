using UnityEngine;
using System.Collections.Generic;

namespace GrazingShmup
{
    public class EnemyFactory : IEnemyFactory
    {
        #region Fields

        Dictionary<EnemyType, EnemyMovementData> _movementData = new Dictionary<EnemyType, EnemyMovementData>();
        Dictionary<EnemyType, BulletData> _bulletData = new Dictionary<EnemyType, BulletData>();

        #endregion


        #region IEnemyFactory

        public Enemy CreateEnemy(EnemyType type)
        {
            if (!_movementData.ContainsKey(type))
            {
                string path = References.Enemy_Movement_Data + ((int)type).ToString();
                _movementData.Add(type, Resources.Load<EnemyMovementData>(path));
            }

            if (!_bulletData.ContainsKey(type))
            {
                string path = References.Enemy_Bullet_Data + ((int)type).ToString();
                _bulletData.Add(type, Resources.Load<BulletData>(path));
            }

            IProjectile bullet = ServiceLocator.GetService<BulletFactory>().GetBullet(
                                                                       _bulletData[type].BaseBullet, _bulletData[type].BulletComponents, _bulletData[type].BulletOwner);

            IWeaponEnemy weapon;
            if (_bulletData[type].IsTracking)
                weapon = new EnemyWeaponTracking(_bulletData[type].BulletConfig, bullet);
            else
                weapon = new EnemyWeaponNonTracking(_bulletData[type].BulletConfig, bullet);

            Enemy enemy = new Enemy( type, weapon, new EnemyEngine(_movementData[type]));

            return enemy;
        }

        public GameObject GetEnemyPrefab(EnemyType type)
        {
            string path = References.Enemy_Prefabs + ((int)type).ToString();
            return Resources.Load<GameObject>(path);
        }

        #endregion
    }
}