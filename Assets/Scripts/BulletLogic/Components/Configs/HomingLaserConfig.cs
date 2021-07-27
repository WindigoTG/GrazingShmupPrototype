using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct HomingLaserConfig
    {
        [Header("Homing Laser settings")]
        [Min(0)] [Tooltip("How long a homing laser tracks target")] public float HomingTime;
        [Tooltip("How sharply a homing laser steers towards target")] public float HomingSpeed;
    }
}