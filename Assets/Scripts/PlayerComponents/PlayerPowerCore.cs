using UnityEngine;
using UnityEngine.UI;

namespace GrazingShmup
{
    public class PlayerPowerCore : IUpdateableRegular
    {
        #region Fields

        Transform _playerShip;
        Transform _grazeSparksTransform;
        ParticleSystem _grazeSparks;

        float _grazeCoolDown = 0.05f;
        float _currentGrazeCoolDown;
        bool _isGrazing;
        float _timeToPowerDown = 0.5f;
        float _currentPowerUpTime;

        float _maxPowerLevel = 100f;
        float _currentPowerLevel;
        float _powerGain = 1f;
        float _powerDecay = 20f;

        Slider _slider;

        #endregion


        #region Properties

        public bool IsGrazing => _isGrazing;

        public int PowerLevel => (int)(_currentPowerLevel / 25);

        #endregion


        #region ClassLifeCycles

        public PlayerPowerCore(Transform playerShip)
        {
            _grazeSparksTransform = Object.Instantiate(Resources.Load<GameObject>(References.Graze_Sparks_Prefab)).transform;
            _grazeSparks = _grazeSparksTransform.GetComponent<ParticleSystem>();
            _playerShip = playerShip;

            _slider = Object.FindObjectOfType<Slider>();
        }

        #endregion


        #region IUpdateableRegular

        public void UpdateRegular(float deltaTime)
        {

            if (_currentGrazeCoolDown > 0)
                _currentGrazeCoolDown -= deltaTime;
            if (_currentPowerUpTime > 0)
                _currentPowerUpTime -= deltaTime;
            else if (_currentPowerLevel > 0)
                _currentPowerLevel -= _powerDecay * deltaTime;
            if (_currentPowerLevel < 0)
                _currentPowerLevel = 0;

            _slider.value = _currentPowerLevel;
            ColorBlock colors = _slider.colors;
            if (_currentPowerLevel == 100)
                colors.disabledColor = Color.red;
            if(_currentPowerLevel < 100)
                colors.disabledColor = Color.magenta;
            if (_currentPowerLevel < 75)
                colors.disabledColor = Color.blue;
            if (_currentPowerLevel < 50)
                colors.disabledColor = Color.cyan;
            if (_currentPowerLevel < 25)
                colors.disabledColor = Color.yellow;
            _slider.colors = colors;
        }

        #endregion


        #region Methods

        public void Hit()
        {
            Debug.LogWarning("Got Hit");
        }

        public void Graze(Vector3 contactPosition)
        {
            //Debug.LogWarning("Got Grazed");

            if (_currentGrazeCoolDown <= 0)
            {
                _currentGrazeCoolDown = _grazeCoolDown;
                _isGrazing = true;
                _currentPowerUpTime = _timeToPowerDown;

                _grazeSparksTransform.position = contactPosition;
                Quaternion rotation = new Quaternion();
                rotation.SetLookRotation(Vector3.forward, contactPosition - _playerShip.position);
                _grazeSparksTransform.rotation = rotation;
                _grazeSparks.Play();

                if (_currentPowerLevel < _maxPowerLevel)
                    _currentPowerLevel += _powerGain;
                else if (_currentPowerLevel > _maxPowerLevel)
                    _currentPowerLevel = _maxPowerLevel;
            }
        }

        #endregion
    }
}