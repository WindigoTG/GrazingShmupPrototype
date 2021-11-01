using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GrazingShmup
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] EnemyRoute _testEnemyRoute;

        List<IUpdateableRegular> _updatables;
        List<IUpdateableFixed> _fixedUpdatables;
        List<IUpdateableLate> _lateUpdatables;

        float _deltaTime;

        [Inject] PlayerController _playerController;
        [Inject] BulletManager _bulletManager;
        [Inject] EnemyController _enemyController;

        [Inject]
        private void Init()
        {
            _updatables = new List<IUpdateableRegular>();
            _fixedUpdatables = new List<IUpdateableFixed>();
            _lateUpdatables = new List<IUpdateableLate>();

            _updatables.Add(_playerController);
            _fixedUpdatables.Add(_playerController);

            _lateUpdatables.Add(_bulletManager);

            _updatables.Add(_enemyController);
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
            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].LateUpdate(Time.deltaTime);
        }
    }
}