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
        [SerializeField][Tooltip("How fast a capsule turns")] float _capsuleTurnSpeed;

        public float CapsuleTurnSpeed
        {
            get => _capsuleTurnSpeed * Mathf.PI / 180;
            set => _capsuleTurnSpeed = value * 180 / Mathf.PI;
        }
    }
}
