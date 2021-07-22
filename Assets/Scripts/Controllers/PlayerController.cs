using UnityEngine;

namespace GrazingShmup
{
    public class PlayerController : IUpdateableRegular, IUpdateableFixed
    {
        private PlayerShip _player;

        private IPlayerFactory _playerFactory;

        private float _inputHor;
        private float _inputVer;
        private float _slowDownInput;

        public PlayerController(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
            _player = _playerFactory.CreatePlayer();
        }

        public void Update(float deltaTime)
        {
            _inputHor = Input.GetAxisRaw(References.Input_Axis_Horizontal);
            _inputVer = Input.GetAxisRaw(References.Input_Axis_Vertical);
            _slowDownInput = Input.GetAxisRaw(References.Input_Axis_Fire2);

            if (Input.GetAxisRaw(References.Input_Axis_Fire) > 0)
            {
                _player.Fire();
            } 
        }
        public void FixedUpdate(float fixedDeltaTime)
        {
            _player.Move(_inputHor, _inputVer, _slowDownInput != 0, fixedDeltaTime);
        }

        public Vector3 PlayerPosition => _player.Transform.position;
    }
}