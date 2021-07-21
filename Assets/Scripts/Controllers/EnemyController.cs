using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class EnemyController : IUpdateableRegular
    {
        Dictionary<EnemyType, List<Enemy>> _enemies = new Dictionary<EnemyType, List<Enemy>>();
        IEnemyFactory _enemyFactory;
        PlayerController _playerController;

        public EnemyController(IEnemyFactory enemyFactory, EnemyRoute route, PlayerController playerController)
        {
            _playerController = playerController;
            _enemyFactory = enemyFactory;

            Enemy enemy = _enemyFactory.CreateEnemy(route.EnemyType);
            enemy.ActivateEnemy(route.GetRoute(),_playerController);

            _enemies.Add(route.EnemyType, new List<Enemy>());
            _enemies[route.EnemyType].Add(enemy);
        }

        public void Update(float deltaTime)
        {
            if (_enemies.Count > 0)
            {
                foreach (var e in _enemies)
                {
                    for (int i = 0; i < e.Value.Count; i++)
                        e.Value[i].Update(deltaTime);
                }
            }
        }
    }
}