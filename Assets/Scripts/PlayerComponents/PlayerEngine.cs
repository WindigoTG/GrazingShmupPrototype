using UnityEngine;

namespace GrazingShmup
{
    public class PlayerEngine : IEnginePlayer
    {
        Transform _playerShip;
        PlayerData _playerData;

        Vector3 _lowerLeft;
        Vector3 _upperRight;

        public PlayerEngine() { }

        public PlayerEngine(Transform playerShip, PlayerData playerData)
        {
            _playerData = playerData;
            _playerShip = playerShip;
        }

        public void SetDependencies(Transform playerShip, PlayerData playerData)
        {
            _playerData = playerData;
            _playerShip = playerShip;
        }

        public void Move(float inputHor, float inputVer, float deltaTime)
        {
            _playerShip.Translate(new Vector3(inputHor, inputVer, 0) * (_playerData.Speed * deltaTime));
            ConstrainPlayerPosition();
        }

        private void ConstrainPlayerPosition()
        {
            _lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
            _upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));

            if (_playerShip.position.x > _upperRight.x - _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(x: _upperRight.x - _playerData.ScreenBoundMargin);
            if (_playerShip.position.x < _lowerLeft.x + _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(x: _lowerLeft.x + _playerData.ScreenBoundMargin);

            if (_playerShip.position.y > _upperRight.y - _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(y: _upperRight.y - _playerData.ScreenBoundMargin);
            if (_playerShip.position.y < _lowerLeft.y + _playerData.ScreenBoundMargin)
                _playerShip.position = _playerShip.position.Change(y: _lowerLeft.y + _playerData.ScreenBoundMargin);
        }
    }
}