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
        [SerializeField] [Tooltip("How fast a capsule turns")]float _rCapsuleTurnSpeed;
        [Min(0)] [Tooltip("Time between capsule's shots")] public float RCapsuleRefireTime;

        public float RCapsuleTurnSpeed
        {
            get => _rCapsuleTurnSpeed * Mathf.PI / 180;
            set => _rCapsuleTurnSpeed = value * 180 / Mathf.PI;
        }
    }
}