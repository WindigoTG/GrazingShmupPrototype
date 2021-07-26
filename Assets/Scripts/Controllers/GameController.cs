using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] EnemyRoute _testEnemyRoute;

        List<IUpdateableRegular> _updatables;
        List<IUpdateableFixed> _fixedUpdatables;
        List<IUpdateableLate> _lateUpdatables;

        float _deltaTime;

        void Awake()
        {
            _updatables = new List<IUpdateableRegular>();
            _fixedUpdatables = new List<IUpdateableFixed>();
            _lateUpdatables = new List<IUpdateableLate>();

            CreateUpdatables();
        }

        void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdatables.Count; i++)
                _fixedUpdatables[i].FixedUpdate(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            if (Time.frameCount % 2 == 0)
                for (int i = 0; i < _lateUpdatables.Count; i++)
                    _lateUpdatables[i].LateUpdate(_deltaTime + Time.deltaTime);
            else
                _deltaTime = Time.deltaTime;
        }

        private void CreateUpdatables()
        {
            ServiceLocator.AddService(new CollisionManager());
            ServiceLocator.AddService(new BulletFactory());

            PlayerController playerController = new PlayerController(new PlayerFactory());
            _updatables.Add(playerController);
            _fixedUpdatables.Add(playerController);

            EnemyFactory enemyFactory = new EnemyFactory();
            ServiceLocator.AddService(new ObjectPoolManager(enemyFactory));

            BulletManager bulletManager = new BulletManager();
            ServiceLocator.AddService(bulletManager);
            _lateUpdatables.Add(bulletManager);

            EnemyController enemyController = new EnemyController(enemyFactory, _testEnemyRoute, playerController);
            _updatables.Add(enemyController);

            ServiceLocator.AddService(new PlayerTracker(playerController.PlayerTransform));
        }
    }
}