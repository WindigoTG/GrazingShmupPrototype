using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct BulletConfig
    {
        #region Fields

        [Header("Base projectile settings")]
        [SerializeField] [Min(0)] [Tooltip("How long a bullet/laser exist")] float _bulletLifeTime;
        [SerializeField] [Min(0)] [Tooltip("Time between shots")] float _fireDelay;
        [Space]
        [Tooltip("Initial speed of a bullet/laser")] public float BulletSpeed;
        [Tooltip("Rate with which bullet's/laser's speed changes per second")] public float BulletDeltaSpeed;
        [SerializeField] [Min(0)] [Tooltip("Delay before bullet's/laser's speed start to change")] float _bulletDeltaSpeedDelay;
        [Space]
        [Tooltip("For Bullet: How fast it turns\n" +
                 "For Homing Laser: How sharply it steers towards target")] public float BulletTurnSpeed;
        [Space]
        [Tooltip("Prefab to use as a bullet")] public GameObject BulletPrefab;
        [Space]
        [Header("Homing Laser settings")]
        [SerializeField] [Min(0)] [Tooltip("How long a homing laser tracks target")] float _homingTime;
        [Space]
        [Tooltip("Prefab to use as a homing laser")] public GameObject HomingLaserPrefab;
        [Space]
        [Header("Out of screen bounds settings")]
        [Tooltip("Should a bullet stay alive after leaving the screen\n" +
                 "True: a bullet will live until its life time runs out\n" +
                 "False: a bullet is destroyed after leaving the screen")] public bool ShouldLiveOffscreen;
        [Min(0)] [Tooltip("An extra distance a bullet can travel off the screen before it is destroyed\n" +
                          "Recommended to set offcet to at least half of bullet's size," +
                          "so a bullet can fully get out of sight before being destroyed")] public float OffscreenOffset;
        [Space]

        public LineConfig LineSettings;
        public ArcConfig ArcSettings;
        public RowConfig RowSettings;
        public CapsuleConfig CapsuleSettings;
        public RepeaterCapsuleConfig RepeaterCapsuleSettings;
        public BurstCapsuleConfig BurstCapsuleSettings;
        public SpinningCapsuleConfig SpinningCapsuleSettings;
        
        private Vector3 _position;
        private Vector3 _rotation;
        private float _angle;

        #endregion

        #region General Properties

        public float BulletLifeTime
        {
            get => _bulletLifeTime;
            set => _bulletLifeTime = Math.Abs(value);
        }

        public float FireDelay
        {
            get => _fireDelay;
            set => _fireDelay = Math.Abs(value);
        }

        public float BulletDeltaSpeedDelay
        {
            get => _bulletDeltaSpeedDelay;
            set => _bulletDeltaSpeedDelay = Math.Abs(value);
        }
         
        public float HomingTime
        {
            get => _homingTime;
            set => _homingTime = Math.Abs(value);
        }

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
            BulletTurnSpeed *= modifier;
            CapsuleSettings.CapsuleTurnSpeed *= modifier;
            BurstCapsuleSettings.BCapsuleTurnSpeed *= modifier;
            RepeaterCapsuleSettings.RCapsuleTurnSpeed *= modifier;
            SpinningCapsuleSettings.SCapsuleTurnSpeed *= modifier;
        }

        public void ModifySpeed (float deltaSpeed)
        {
            BulletSpeed += deltaSpeed;
            CapsuleSettings.CapsuleSpeed += deltaSpeed;
            BurstCapsuleSettings.BCapsuleSpeed += deltaSpeed;
            RepeaterCapsuleSettings.RCapsuleSpeed += deltaSpeed;
            SpinningCapsuleSettings.SCapsuleSpeed += deltaSpeed;
        }

        public void InvertSpinCapsuleSpin()
        {
            SpinningCapsuleSettings.SpinSpeed = -SpinningCapsuleSettings.SpinSpeed;
        }

        public void ModifyDeltaSpeedDelay(float difference)
        {
            BulletDeltaSpeedDelay += difference;
            CapsuleSettings.CapsuleDeltaSpeedDelay += difference;
            BurstCapsuleSettings.BCapsuleDeltaSpeedDelay += difference;
            RepeaterCapsuleSettings.RCapsuleDeltaSpeedDelay += difference;
            SpinningCapsuleSettings.SCapsuleDeltaSpeedDelay += difference;
        }

        #endregion
    }
}