using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct ProjectileConfig
    {
        #region Fields
        [Header("Base projectile settings")]
        [SerializeField] [Min(0)] [Tooltip("How long a bullet/laser exist")] float _projectileLifeTime;
        [SerializeField] [Min(0)] [Tooltip("Time between shots")] float _fireDelay;
        [Space]
        [SerializeField] [Tooltip("Initial speed of a bullet/laser")] float _projectileSpeed;
        [SerializeField] [Tooltip("Rate with which bullet's/laser's speed changes per second")] float _projectileDeltaSpeed;
        [SerializeField] [Min(0)] [Tooltip("Delay before bullet's/laser's speed start to change")] float _projectileDeltaSpeedDelay;
        [Space]
        [SerializeField]
        [Tooltip("For Bullet: How fast it turns\n" +
                 "For Homing Laser: How sharply it steers towards target")] float _projectileTurnSpeed;
        [Space]
        [SerializeField] [Tooltip("Prefab to use as a bullet")] private GameObject _bulletPrefab;
        [Space]
        [Header("Homing Laser settings")]
        [SerializeField] [Min(0)] [Tooltip("How long a homing laser tracks target")] float _homingTime;
        [Space]
        [SerializeField] [Tooltip("Prefab to use as a homing laser")] private GameObject _homingLaserPrefab;
        [Space]

        [SerializeField] LineConfig _lineSettings;
        [SerializeField] ArcConfig _arcSettings;
        [SerializeField] RowConfig _rowSettings;
        [SerializeField] CapsuleConfig _capsuleSettings;
        [SerializeField] RepeaterCapsuleConfig _repeaterCapsuleSettings;
        [SerializeField] BurstCapsuleConfig _burstCapsuleSettings;
        [SerializeField] SpinningCapsuleConfig _spinningCapsuleSettings;
        
        private Vector3 _position;
        private Vector3 _rotation;
        private float _angle;
        #endregion

        #region General Properties
        public float ProjectileLifeTime
        {
            get => _projectileLifeTime;
            set => _projectileLifeTime = value;
        }

        public float FireDelay
        {
            get => _fireDelay;
            set => _fireDelay = Math.Abs(value);
        }

        public float ProjectileSpeed
        {
            get => _projectileSpeed;
            set => _projectileSpeed = value;
        }

        public float ProjectileDeltaSpeed
        {
            get => _projectileDeltaSpeed;
            set => _projectileDeltaSpeed = value;
        }

        public float ProjectileDeltaSpeedDelay
        {
            get => _projectileDeltaSpeedDelay;
            set => _projectileDeltaSpeedDelay = value;
        }

        public float ProjectileTurnSpeed
        {
            get => _projectileTurnSpeed * Mathf.PI / 180;
            set => _projectileTurnSpeed = value * 180 / Mathf.PI;
        }
         
        public float HomingTime
        {
            get => _homingTime;
            set => _homingTime = value;
        }

        public GameObject BulletPrefab => _bulletPrefab;

        public GameObject HomingLaserPrefab => _homingLaserPrefab;
        #endregion

        #region Components Properties


        public LineConfig LineSettings => _lineSettings;

        public ArcConfig ArcSettings => _arcSettings;

        public RowConfig RowSettings => _rowSettings;

        public CapsuleConfig CapsuleSettings => _capsuleSettings;

        public RepeaterCapsuleConfig RepeaterCapsuleSettings => _repeaterCapsuleSettings;

        public BurstCapsuleConfig BurstCapsuleSettings => _burstCapsuleSettings;

        public SpinningCapsuleConfig SpinningCapsuleSettings => _spinningCapsuleSettings;
        #endregion

        #region Position/Rotation Properties
        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }

        public Vector3 Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }

        public float Angle
        {
            get => _angle;
            set => _angle = value;
        }
        #endregion

        #region Config modifying methods
        public void ModifyTurning(int modifier)
        {
            _projectileTurnSpeed *= modifier;
            _capsuleSettings.CapsuleTurnSpeed *= modifier;
            _burstCapsuleSettings.BCapsuleTurnSpeed *= modifier;
            _repeaterCapsuleSettings.RCapsuleTurnSpeed *= modifier;
            _spinningCapsuleSettings.SCapsuleTurnSpeed *= modifier;
        }

        public void ModifySpeed (float deltaSpeed)
        {
            _projectileSpeed += deltaSpeed;
            _capsuleSettings.CapsuleSpeed += deltaSpeed;
            _burstCapsuleSettings.BCapsuleSpeed += deltaSpeed;
            _repeaterCapsuleSettings.RCapsuleSpeed += deltaSpeed;
            _spinningCapsuleSettings.SCapsuleSpeed += deltaSpeed;
        }

        public void InvertSpinCapsuleSpin()
        {
            _spinningCapsuleSettings.SpinSpeed = -_spinningCapsuleSettings.SpinSpeed;
        }

        public void ModifyDeltaSpeedDelay(float difference)
        {
            _projectileDeltaSpeedDelay += difference;
            _capsuleSettings.CapsuleDeltaSpeedDelay += difference;
            _burstCapsuleSettings.BCapsuleDeltaSpeedDelay += difference;
            _repeaterCapsuleSettings.RCapsuleDeltaSpeedDelay += difference;
            _spinningCapsuleSettings.SCapsuleDeltaSpeedDelay += difference;
        }
        #endregion
    }
}