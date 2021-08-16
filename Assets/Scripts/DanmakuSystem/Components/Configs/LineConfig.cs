using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct LineConfig
    {
        [Header("Line settings")]
        [Min(1)] [Tooltip("Number of bullets in a line")] public int LineBulletCount;
        [Tooltip("Difference in initial speed between bullets in a line")] public float DeltaSpeedInLine;
    }
}