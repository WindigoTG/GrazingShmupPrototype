using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct RepeaterCapsuleConfig
    {
        [Header("Repeater Capsule settings")]
        [SerializeField] [Min(0)] [Tooltip("How long a capsule exist")] private float _rCapsuleLifeTime;
        [Tooltip("Initial speed of a capsule")] public float RCapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float RCapsuleDeltaSpeed;
        [SerializeField] [Min(0)] [Tooltip("Delay before capsule's speed start to change")] private float _rCapsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float RCapsuleTurnSpeed;
        [SerializeField] [Min(0)] [Tooltip("Time between capsule's shots")] private float _rCapsuleRefireTime;
        [Tooltip("Attempt to adjust DeltaSpeedDelay in shots," +
            "so that they start to change speed at the same time")] public bool SynchronizeDeltaSpeedDelayInShots;
        [Space]
        [Tooltip("Prefab to use as a capsule")] public GameObject RCapsulePrefab;

        public float RCapsuleLifeTime
        {
            get => _rCapsuleLifeTime;
            set => _rCapsuleLifeTime = Math.Abs(value);
        }

        public float RCapsuleDeltaSpeedDelay
        {
            get => _rCapsuleDeltaSpeedDelay;
            set => _rCapsuleDeltaSpeedDelay = Math.Abs(value);
        }

        public float RCapsuleRefireTime
        {
            get => _rCapsuleRefireTime;
            set => _rCapsuleRefireTime = Math.Abs(value);
        }
    }
}