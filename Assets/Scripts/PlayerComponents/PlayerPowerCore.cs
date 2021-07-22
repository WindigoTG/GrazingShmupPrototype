using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class PlayerPowerCore 
    {
        Transform _playerShip;
        Transform _grazeSparksTransform;
        ParticleSystem _grazeSparks;

        float _grazeCoolDown = 0.05f;
        float _currentGrazeCoolDown;
        bool _isGrazing;
        float _timeToPowerDown = 0.2f;
        float _currentPowerUpTime;

        public PlayerPowerCore(Transform playerShip)
        {
            _grazeSparksTransform = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GrazeSparks")).transform;
            _grazeSparks = _grazeSparksTransform.GetComponent<ParticleSystem>();
            _playerShip = playerShip;
        }

        public void Update(float deltaTime)
        {

            if (_currentGrazeCoolDown > 0)
                _currentGrazeCoolDown -= deltaTime;
            if (_currentPowerUpTime > 0)
                _currentPowerUpTime -= deltaTime;
            else if (_isGrazing)
                _isGrazing = false;
        }

        public bool IsGrazing => _isGrazing;

        public void Hit()
        {
            Debug.LogError("Got Hit");
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
            }
        }
    }
}