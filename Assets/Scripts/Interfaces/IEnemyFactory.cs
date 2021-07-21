using UnityEngine;

namespace GrazingShmup
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(EnemyType type);
        GameObject GetEnemyPrefab(EnemyType type);
    }
}