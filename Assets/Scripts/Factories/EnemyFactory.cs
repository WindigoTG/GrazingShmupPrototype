using UnityEngine;
using System.Collections.Generic;

namespace GrazingShmup
{
    public class EnemyFactory : IEnemyFactory
    {
        Dictionary<EnemyType, EnemyMovementData> _movementData = new Dictionary<EnemyType, EnemyMovementData>();
        Dictionary<EnemyType, BulletData> _bulletData = new Dictionary<EnemyType, BulletData>();

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

            IFireable bullet = ServiceLocator.GetService<BulletFactory>().GetBullet(
                                                                        _bulletData[type].BulletComponents, _bulletData[type].BulletOwner);

            Enemy enemy = new Enemy( type,
                new EnemyWeaponTracking(_bulletData[type].Config, bullet),
                new EnemyEngine(_movementData[type]));

            return enemy;
        }

        public GameObject GetEnemyPrefab(EnemyType type)
        {
            string path = References.Enemy_Prefabs + ((int)type).ToString();
            return Resources.Load<GameObject>(path);
        }
    }
}