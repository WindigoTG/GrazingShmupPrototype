using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct BurstCapsuleConfig
    {
        #region Fields

        [Header("Burst Capsule settings")]
        [SerializeField] [Min(0)] [Tooltip("How long a capsule exist")] private float _bCapsuleLifeTime;
        [SerializeField] [Min(0)] [Tooltip("Time between shots in burst")] private float _burstRefireTime;
        [Tooltip("Difference in initial speed between bullets in a burst")] public float DeltaSpeedInBurst;
        [Tooltip("If true, burst is aimed at target")] public bool IsTracking;
        [Tooltip("Initial speed of a capsule")] public float BCapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float BCapsuleDeltaSpeed;
        [SerializeField] [Min(0)] [Tooltip("Delay before capsule's speed start to change")] private float _bCapsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float BCapsuleTurnSpeed;
        [Space]
        [Tooltip("Prefab to use as a capsule")] public GameObject BCapsulePrefab;

        #endregion


        #region Properties

        public float BCapsuleLifeTime
        {
            get => _bCapsuleLifeTime;
            set => _bCapsuleLifeTime = Math.Abs(value);
        }

        public float BurstRefireTime
        {
            get => _burstRefireTime;
            set => _burstRefireTime = Math.Abs(value);
        }

        public float BCapsuleDeltaSpeedDelay
        {
            get => _bCapsuleDeltaSpeedDelay;
            set => _bCapsuleDeltaSpeedDelay = Math.Abs(value);
        }

        #endregion
    }
}
