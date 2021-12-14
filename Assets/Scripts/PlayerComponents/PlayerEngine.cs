using UnityEngine;

namespace GrazingShmup
{
    public class PlayerEngine : IEnginePlayer
    {
        #region Fields

        Transform _playerShip;
        PlayerData _playerData;

        #endregion


        #region ClassLifeCycles

        public PlayerEngine() { }

        public PlayerEngine(Transform playerShip, PlayerData playerData)
        {
            _playerData = playerData;
            _playerShip = playerShip;
        }

        #endregion


        #region IEnginePlayer

        public void SetDependencies(Transform playerShip, PlayerData playerData)
        {
            _playerData = playerData;
            _playerShip = playerShip;
        }

        public void Move(float inputHor, float inputVer,  bool isSlowedDown, float deltaTime)
        {
            _playerShip.Translate(new Vector3(inputHor, inputVer, 0) * 
                                    ((isSlowedDown ? _playerData.Speed * _playerData.SlowDownRate : _playerData.Speed) * deltaTime));
            ConstrainPlayerPosition();
        }

        #endregion


        #region Methods

        private void ConstrainPlayerPosition()
        {
            if (_playerShip.position.x > ScreenBounds.RightBound - _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(x: ScreenBounds.RightBound - _playerData.ScreenBoundMargin);
            if (_playerShip.position.x < ScreenBounds.LeftBound + _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(x: ScreenBounds.LeftBound + _playerData.ScreenBoundMargin);

            if (_playerShip.position.y > ScreenBounds.TopBound - _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(y: ScreenBounds.TopBound - _playerData.ScreenBoundMargin);
            if (_playerShip.position.y < ScreenBounds.BottomBound + _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(y: ScreenBounds.BottomBound + _playerData.ScreenBoundMargin);
        }

        #endregion
    }
}