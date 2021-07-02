using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class GameController : MonoBehaviour
    {

        List<IUpdateableRegular> _updatables;
        List<IUpdateableFixed> _fixedUpdatables;

        void Awake()
        {
            _updatables = new List<IUpdateableRegular>();
            _fixedUpdatables = new List<IUpdateableFixed>();

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

        private void CreateUpdatables()
        {
            PlayerController playerController = new PlayerController(new PlayerFactory());
            _updatables.Add(playerController);
            _fixedUpdatables.Add(playerController);
        }
    }
}