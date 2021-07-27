using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct RowConfig
    {
        [Header("Row settings")]
        [Min(0)]
        [Tooltip("Number of additional bullets in an row" +
                                            "\n0 means only the default single bullet will be shot")]
        public int RowAdditionalBulletCount;

        [Tooltip("Horisontal offset of additional bullets")] public float RowHorisontalOffset;
        [Tooltip("Vertiacal offset of additional bullets")] public float RowVerticalOffset;
        [Tooltip("If true, additional bullets are added on both sides, with the same offset")] public bool IsTwoSided;
        [Tooltip("If true, additional bullets are added on both sides, in symmetrical fashion")] public bool IsMirrored;
        [Min(0)] [Tooltip("If mirrored, puts a gap between left and right sires")] public float RowGap;
    }
}
