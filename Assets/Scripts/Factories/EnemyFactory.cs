using UnityEngine;
using System.IO;
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
                string path = ConstantsAndMagicLines.Enemy_Movement_Data + ((int)type).ToString();
                _movementData.Add(type, Resources.Load<EnemyMovementData>(path));
            }

            if (!_bulletData.ContainsKey(type))
            {
                string path = ConstantsAndMagicLines.Enemy_Bullet_Data + ((int)type).ToString();
                Debug.Log(path);
                _bulletData.Add(type, Resources.Load<BulletData>(path));
            }

            IFireable bullet = new SingleBullet(BulletOwner.Enemy).FiredInRow().FiredInArc().FiredInLine().FiredInDelayedCapsule();

            Enemy enemy = new Enemy( type,
                new EnemyWeaponTracking(_bulletData[type].GetConfig(), bullet),
                new EnemyEngine(_movementData[type]));

            return enemy;
        }

        public GameObject GetEnemyPrefab(EnemyType type)
        {
            string path = ConstantsAndMagicLines.Enemy_Prefabs + ((int)type).ToString();
            return Resources.Load<GameObject>(path);
        }
    }
}