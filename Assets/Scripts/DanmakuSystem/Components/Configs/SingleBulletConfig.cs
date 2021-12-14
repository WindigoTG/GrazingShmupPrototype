using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct SingleBulletConfig
    {
        #region Fields

        [Header("Single bullet settings")]
        [Tooltip("Initial speed of a bullet")] public float BulletSpeed;
        [Tooltip("Rate with which bullet's speed changes per second")] public float BulletDeltaSpeed;
        [Min(0)] [Tooltip("Delay before bullet's speed start to change")] public float BulletDeltaSpeedDelay;
        [Tooltip("How fast a bullet turns")] public float BulletTurnSpeed;

        #endregion
    }
}