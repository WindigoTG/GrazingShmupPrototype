using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct CapsuleConfig
    {
        [Header("Capsule settings")]
        [Min(0)] [Tooltip("How long a capsule exist")] public float CapsuleLifeTime;
        [Tooltip("Initial speed of a capsule")] public float CapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float CapsuleDeltaSpeed;
        [Min(0)] [Tooltip("Delay before capsule's speed start to change")] public float CapsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float CapsuleTurnSpeed;
        [Space]
        [SerializeField] [Tooltip("Prefab to use as a capsule")] private GameObject _capsulePrefab;

        public GameObject CapsulePrefab => _capsulePrefab;
    }
}
