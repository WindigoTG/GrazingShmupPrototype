using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct RepeaterCapsuleConfig
    {
        [Header("Repeater Capsule settings")]
        [Min(0)] [Tooltip("How long a capsule exist")] public float RCapsuleLifeTime;
        [Tooltip("Initial speed of a capsule")] public float RCapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float RCapsuleDeltaSpeed;
        [Min(0)] [Tooltip("Delay before capsule's speed start to change")] public float RCapsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float RCapsuleTurnSpeed;
        [Min(0)] [Tooltip("Time between capsule's shots")] public float RCapsuleRefireTime;
        [Tooltip("Attempt to adjust DeltaSpeedDelay in shots," +
            "so that they start to change speed at the same time")] public bool SynchronizeDeltaSpeedDelayInShots;
        [Space]
        [SerializeField] [Tooltip("Prefab to use as a capsule")] private GameObject _rCapsulePrefab;

        public GameObject RCapsulePrefab => _rCapsulePrefab;
    }
}