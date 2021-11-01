using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct CapsuleConfig
    {
        [Header("Capsule settings")]
        [SerializeField] [Min(0)] [Tooltip("How long a capsule exist")] private float _capsuleLifeTime;
        [Tooltip("Initial speed of a capsule")] public float CapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float CapsuleDeltaSpeed;
        [SerializeField] [Min(0)] [Tooltip("Delay before capsule's speed start to change")] private float _capsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float CapsuleTurnSpeed;
        [Space]
        [Tooltip("Prefab to use as a capsule")] public GameObject CapsulePrefab;

        public float CapsuleLifeTime
        {
            get => _capsuleLifeTime;
            set => _capsuleLifeTime = Math.Abs(value);
        }

        public float CapsuleDeltaSpeedDelay
        {
            get => _capsuleDeltaSpeedDelay;
            set => _capsuleDeltaSpeedDelay = Math.Abs(value);
        }
    }
}
