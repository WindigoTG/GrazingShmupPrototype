using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct BurstCapsuleConfig
    {
        [Header("Burst Capsule settings")]
        [Min(0)] [Tooltip("How long a capsule exist")] public float BCapsuleLifeTime;
        [Min(0)] [Tooltip("Time between shots in burst")] public float BurstRefireTime;
        [Tooltip("Difference in initial speed between bullets in a burst")] public float DeltaSpeedInBurst;
        [Tooltip("If true, burst is aimed at target")] public bool IsTracking;
        [Tooltip("Initial speed of a capsule")] public float BCapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float BCapsuleDeltaSpeed;
        [Min(0)] [Tooltip("Delay before capsule's speed start to change")] public float BCapsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float BCapsuleTurnSpeed;
        [Space]
        [SerializeField] [Tooltip("Prefab to use as a capsule")] private GameObject _bCapsulePrefab;

        public GameObject BCapsulePrefab => _bCapsulePrefab;
    }
}
