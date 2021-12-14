using UnityEngine;

namespace GrazingShmup
{
    public class PlayerController : IUpdateableRegular, IUpdateableFixed
    {
        #region Fields

        private PlayerShip _player;

        private IPlayerFactory _playerFactory;

        private float _inputHor;
        private float _inputVer;
        private float _slowDownInput;

        #endregion


        #region Properties

        public Vector3 PlayerPosition => _player.Transform.position;
        public Transform PlayerTransform => _player.Transform;

        #endregion


        #region ClassLifeCycles

        public PlayerController(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
            _player = _playerFactory.CreatePlayer();
        }

        #endregion


        #region IUpdateableRegular

        public void UpdateRegular(float deltaTime)
        {
            _inputHor = Input.GetAxisRaw(References.Input_Axis_Horizontal);
            _inputVer = Input.GetAxisRaw(References.Input_Axis_Vertical);
            _slowDownInput = Input.GetAxisRaw(References.Input_Axis_Fire2);

            if (Input.GetAxisRaw(References.Input_Axis_Fire) > 0)
            {
                _player.Fire();
            } 
        }

        #endregion


        #region IUpdateableFixed

        public void UpdateFixed(float fixedDeltaTime)
        {
            _player.Move(_inputHor, _inputVer, _slowDownInput != 0, fixedDeltaTime);
        }

        #endregion
    }
}