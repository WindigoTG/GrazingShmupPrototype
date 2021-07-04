using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class GameController : MonoBehaviour
    {

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
            PlayerController playerController = new PlayerController(new PlayerFactory());
            _updatables.Add(playerController);
            _fixedUpdatables.Add(playerController);

            ServiceLocator.AddService(new CollisionManager());
            ServiceLocator.AddService(new ObjectPoolManager());

            BulletManager bulletManager = new BulletManager();
            ServiceLocator.AddService(bulletManager);
            _lateUpdatables.Add(bulletManager);
        }
    }
}