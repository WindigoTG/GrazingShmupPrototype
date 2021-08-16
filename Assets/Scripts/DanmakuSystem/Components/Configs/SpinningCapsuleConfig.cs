using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct SpinningCapsuleConfig
    {
        [Header("Spinning Capsule settings")]
        [SerializeField] [Tooltip("For how many degrees burst will spin")] public float SpinArc;
        [SerializeField] [Tooltip("How fast a capsule spins")] public float SpinSpeed;
        [SerializeField] [Min(0)] [Tooltip("Ammount of degrees a capsule must spin between each shot")] public float DegreesBetweenShots;
        [Tooltip("Initial speed of a capsule")] public float SCapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float SCapsuleDeltaSpeed;
        [Min(0)] [Tooltip("Delay before capsule's speed start to change")] public float SCapsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float SCapsuleTurnSpeed;
        [Space]
        [SerializeField] [Tooltip("Prefab to use as a capsule")] private GameObject _sCapsulePrefab;

        public GameObject SCapsulePrefab => _sCapsulePrefab;
    }
}
