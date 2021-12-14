using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct SpinningCapsuleConfig
    {
        #region Fields

        [Header("Spinning Capsule settings")]
        [Tooltip("For how many degrees burst will spin")] public float SpinArc;
        [Tooltip("How fast a capsule spins")] public float SpinSpeed;
        [SerializeField] [Min(0)] [Tooltip("Ammount of degrees a capsule must spin between each shot")] private float _degreesBetweenShots;
        [Tooltip("Initial speed of a capsule")] public float SCapsuleSpeed;
        [Tooltip("Rate with which capsule's speed changes per second")] public float SCapsuleDeltaSpeed;
        [SerializeField] [Min(0)] [Tooltip("Delay before capsule's speed start to change")] private float _sCapsuleDeltaSpeedDelay;
        [Tooltip("How fast a capsule turns")] public float SCapsuleTurnSpeed;
        [Space]
        [Tooltip("Prefab to use as a capsule")] public GameObject SCapsulePrefab;

        #endregion


        #region Properties

        public float DegreesBetweenShots
        {
            get => _degreesBetweenShots;
            set => _degreesBetweenShots = Math.Abs(value);
        }

        public float SCapsuleDeltaSpeedDelay
        {
            get => _sCapsuleDeltaSpeedDelay;
            set => _sCapsuleDeltaSpeedDelay = Math.Abs(value);
        }

        #endregion
    }
}
