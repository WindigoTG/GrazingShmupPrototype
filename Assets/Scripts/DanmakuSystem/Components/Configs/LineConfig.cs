using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct LineConfig
    {
        [Header("Line settings")]
        [SerializeField] [Min(1)] [Tooltip("Number of bullets in a line")] int _lineBulletCount;
        [Tooltip("Difference in initial speed between bullets in a line")] public float DeltaSpeedInLine;

        public int LineBulletCount
        {
            get => _lineBulletCount;
            set => _lineBulletCount = value >= 1 ? value : 1;
        }
    }
}