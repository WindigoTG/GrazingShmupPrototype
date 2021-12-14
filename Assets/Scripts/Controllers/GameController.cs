using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class GameController : MonoBehaviour
    {
        #region Fields

        [SerializeField] EnemyRoute _testEnemyRoute;

        List<IUpdateableRegular> _updatables;
        List<IUpdateableFixed> _fixedUpdatables;
        List<IUpdateableLate> _lateUpdatables;

        float _deltaTime;

        #endregion


        #region UnityMethods

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
                _updatables[i].UpdateRegular(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdatables.Count; i++)
                _fixedUpdatables[i].UpdateFixed(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].UpdateLate(Time.deltaTime);

            //if (Time.frameCount % 2 == 0)
            //    for (int i = 0; i < _lateUpdatables.Count; i++)
            //        _lateUpdatables[i].LateUpdate(_deltaTime + Time.deltaTime);
            //else
            //    _deltaTime = Time.deltaTime;
        }

        #endregion


        #region Methods

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

        #endregion
    }
}